using System;
using System.ComponentModel;
using RestSharp;

namespace cs_api_dotnet
{
    /// <summary>
    /// API Access into CaseStack Supply Chain Management Suite.
    /// For more infor please visit http://docs.casestack.io/
    /// </summary>
    public class CaseStackApi
    {
        private string _apiKey = "";
        private string _companyId = "";
        private string _apiEndpoint = "https://app.casestack.io";

        /// <summary>
        /// Available shipment statuses
        /// </summary>
        public enum ShipmentStatus
        {
            [DescriptionAttribute("Broker Approval Pending")]BrokerApprovalPending,
            [DescriptionAttribute("Quote Pending")]QuotePending,
            [DescriptionAttribute("Customer Approval Pending")]CustomerApprovalPending,
            [DescriptionAttribute("Customer Rejected")]CustomerRejected,
            [DescriptionAttribute("Ready to Tender")]ReadytoTender,
            [DescriptionAttribute("Tendered")]Tendered,
            [DescriptionAttribute("Tender Accepted by Carrier")]TenderAcceptedbyCarrier,
            [DescriptionAttribute("Tender Rejected by Carrier")]TenderRejectedbyCarrier,
            [DescriptionAttribute("Pickup Appointment Scheduled")]PickupAppointmentScheduled,
            [DescriptionAttribute("Picked Up")]PickedUp,
            [DescriptionAttribute("In Transit")]InTransit,
            [DescriptionAttribute("Delivery Appointment Scheduled")]DeliveryAppointmentScheduled,
            [DescriptionAttribute("Out for Delivery")]OutforDelivery,
            [DescriptionAttribute("Delivered")]Delivered,
            [DescriptionAttribute("Delivery Exception")]DeliveryException,
            [DescriptionAttribute("Cancelled")]Cancelled,
            [DescriptionAttribute("Archived")]Archived,
            [DescriptionAttribute("Invoiced")]Invoiced,
            [DescriptionAttribute("Billable")]Billable,
        }

        /// <summary>
        /// Authenticate API Access. Your credentials are available under the 'Settings > CaseStack API'.
        /// </summary>
        /// <param name="key">Your API Key</param>
        /// <param name="companyId">Your Company ID</param>
        public void Authenticate(String key, String companyId)
        {
            _apiKey = key;
            _companyId = companyId;
        }

        /// <summary>
        /// Toggle between production or test environments
        /// </summary>
        /// <param name="production">Use production environment</param>
        public void UseProduction(Boolean production)
        {
            _apiEndpoint = production ? "https://app.casestack.io" : "https://staging.casestack.io";
        }

        private RestClient GetRestClient()
        {
            if (String.IsNullOrEmpty(_apiEndpoint) || String.IsNullOrEmpty(_companyId) ||
                String.IsNullOrEmpty(_apiKey))
                throw new ApplicationException(
                    "API Client is not properly Initialized. Please set credentials and environment");

            var client = new RestClient
            {
                BaseUrl = new Uri(_apiEndpoint),
                Authenticator = new HttpBasicAuthenticator(_companyId, _apiKey)
            };
            return client;
        }

        /// <summary>
        /// Get Carrier by ID
        /// </summary>
        /// <param name="carrierId">Carrier ID</param>
        /// <returns>Carrier Object</returns>
        public Carrier GetCarrier(string carrierId)
        {
            if (String.IsNullOrEmpty(carrierId))
                throw new ApplicationException("Carrier ID cannot be empty.");

            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/carrier/" + carrierId,
                RequestFormat = DataFormat.Json,
                RootElement = "Carrier"
            };

            var response = client.Execute<Carrier>(request);
            if (response.ErrorException != null)
                throw new ApplicationException("Error retrieving Carrier", response.ErrorException);

            var carrier = response.Data;
            carrier.restClient = client;
            return carrier;
        }

        /// <summary>
        /// Get Custom Fields for an object type
        /// </summary>
        /// <param name="carrierId">Parent TYpe</param>
        /// <returns>Custom Fields Object</returns>
        public CustomFields GetCustomFields(string parent)
        {
            if (String.IsNullOrEmpty(parent))
                throw new ApplicationException("Parent cannot be empty.");

            string[] allowedParents = { "carrier", "customer", "shipment" };

            int pos = Array.IndexOf(allowedParents, parent);

            if (pos <= -1)
                throw new ApplicationException("Parent can only be 'carrier', 'customer' or 'shipment'");

            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/customfield/" + parent,
                RequestFormat = DataFormat.Json,
                RootElement = "Carrier"
            };

            var response = client.Execute<CustomFields>(request);
            if (response.ErrorException != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    // no custom fields defined for object, so return a blank object
                    CustomFields blank = new CustomFields();
                    blank._id = "";
                    blank.fields = new System.Collections.Generic.List<Field>();
                    blank.parent = parent;
                    return blank;
                } else {
                    throw new ApplicationException("Error retrieving custom fields", response.ErrorException);
                }          
            }
                
            var customFields = response.Data;
            return customFields;
        }

        /// <summary>
        /// Get Customer by ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>Customer Object</returns>
        public Customer GetCustomer(string customerId)
        {
            if (String.IsNullOrEmpty(customerId))
            {
                throw new ApplicationException("Customer ID cannot be empty.");
            }

            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/customer/" + customerId,
                RequestFormat = DataFormat.Json,
                RootElement = "Customer"
            };

            var response = client.Execute<Customer>(request);
            if (response.ErrorException != null)
                throw new ApplicationException("Error retrieving Customer", response.ErrorException);

            var customer = response.Data;
            return customer;
        }

        /// <summary>
        /// Get Shipment by ID
        /// </summary>
        /// <param name="shipmentId">Shipment ID</param>
        /// <returns>Shipment Object</returns>
        public Shipment GetShipment(int shipmentId)
        {
            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/shipment/" + shipmentId,
                RequestFormat = DataFormat.Json,
                RootElement = "Shipment"
            };

            var response = client.Execute<Shipment>(request);
            if (response.ErrorException != null)
                throw new ApplicationException("Error retrieving Shipment", response.ErrorException);

            var shipment = response.Data;
            return shipment;
        }

        /// <summary>
        /// Lock a shipment by ID, makes it read-only from the TMS. 
        /// The shipment can still be updated via the API
        /// </summary>
        /// <param name="shipmentId">Shipment ID</param>
        /// <param name="isLocked">Lock or Unlock</param>
        public void LockShipment(int shipmentId, bool isLocked)
        {
            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/shipment/readonly/" + shipmentId,
                RequestFormat = DataFormat.Json,
                Method = Method.PUT      
            };

            request.AddParameter("readonly", isLocked.ToString().ToLower());

            var response = client.Execute(request);
            if (response.ErrorException != null)
                throw new ApplicationException("Error locking/unlocking Shipment", response.ErrorException);
        }

        /// <summary>
        /// Set status of shipment
        /// </summary>
        /// <param name="shipmentId">Shipment ID</param>
        /// <param name="status">Shipment Status</param>
        public void SetShipmentStatus(int shipmentId, ShipmentStatus status)
        {
            var client = GetRestClient();
            var request = new RestRequest
            {
                Resource = "api/shipment/status/" + shipmentId,
                RequestFormat = DataFormat.Json,
                Method = Method.PUT
            };

            request.AddParameter("status", EnumUtils.stringValueOf(status));

            var response = client.Execute(request);
            if (response.ErrorException != null)
                throw new ApplicationException("Error updating status of Shipment", response.ErrorException);
        }
    }
}
