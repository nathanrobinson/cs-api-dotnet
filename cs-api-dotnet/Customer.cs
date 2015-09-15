using System.Collections.Generic;

namespace cs_api_dotnet
{
    /// <summary>
    /// Customer Object
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        public string customer_id { get; set; }

        /// <summary>
        /// Timestamp the customer was created (unix epoch)
        /// </summary>
        public long created { get; set; }

        /// <summary>
        /// Name of customer
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Whether the customer is active or not
        /// </summary>
        public bool is_active { get; set; }

        /// <summary>
        /// URL to logo of customer
        /// </summary>
        public string logo { get; set; }

        /// <summary>
        /// Customer Rep
        /// </summary>
        public string rep { get; set; }

        /// <summary>
        /// Any comments or special instructions for the customer
        /// </summary>
        public string comments { get; set; }

        public CustomerBilling billing { get; set; }
        public CustomerIdentifiers identifiers { get; set; }
        public List<CustomerContact> contacts { get; set; }
        public Address address { get; set; }

        /// <summary>
        /// Dictionary of custom fields created for object.
        /// </summary>
        public Dictionary<string, string> custom_fields { get; set; }
    }

    /// <summary>
    /// Customer billing info
    /// </summary>
    public class CustomerBilling
    {
        public string accounting_code { get; set; }
        public string payment_terms { get; set; }
        public double credit_limit { get; set; }
    }

    /// <summary>
    /// Customer contact
    /// </summary>
    public class CustomerContact
    {
        public string type { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
    }

    /// <summary>
    /// Customer company identifiers
    /// </summary>
    public class CustomerIdentifiers
    {
        /// <summary>
        /// Employer identification number (Tax ID)
        /// </summary>
        public int ein { get; set; }

        /// <summary>
        /// Dun & Bradstreet Identification number
        /// </summary>
        public int duns { get; set; }

        /// <summary>
        /// Industry
        /// </summary>
        public string industry { get; set; }

        /// <summary>
        /// Customer Type
        /// </summary>
        public string company_type { get; set; }
    }
}
