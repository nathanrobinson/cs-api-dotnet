using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using cs_api_dotnet;
using FakeItEasy;
using RestSharp;

namespace cs_api_dotnet_tests
{
    public class CaseStackApiMock : CaseStackApi
    {
        protected override IRestClient GetRestClient()
        {
            var restClient = A.Fake<IRestClient>();
            var apiCarrierErr = "api/carrier/err";
            var apiCarrierGood = "api/carrier/foo";
            var apiCustomerErr = "api/customer/err";
            var apiCustomerGood = "api/customer/foo";
            var apiCustomFieldsErr = "api/customfield/testerror";
            var apiShipmentErr = "api/shipment/-1";
            var apiShipStatusErr = "api/shipment/status/-1";
            var apiLockShipmentErr = "api/shipment/readonly/-1";
            var apiShipStatusGood = "api/shipment/status/0";
            var apiLockShipmentGood = "api/shipment/readonly/0";
            string apiGetAddressError = "api/address/error";
            string apiGetAddressGood = "api/address/foo";


           
            //SHIPMENTS
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiShipStatusErr ))).Returns(new RestResponse<Shipment> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiLockShipmentErr))).Returns(new RestResponse<Shipment> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiShipStatusGood))).Returns(new RestResponse<Shipment> { Data = new Shipment() });
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiLockShipmentGood ))).Returns(new RestResponse<Shipment> { Data = new Shipment() });
            A.CallTo(() => restClient.Execute<Shipment>(A<IRestRequest>.That.Not.Matches(r=>r.Resource == apiShipmentErr))).Returns(new RestResponse<Shipment>{Data = new Shipment()});
            A.CallTo(() => restClient.Execute<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiShipmentErr))).Returns(new RestResponse<Shipment>{StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test")});

            //CustomFields
            A.CallTo(() => restClient.Execute<CustomFields>(A<IRestRequest>.That.Not.Matches(r => r.Resource.StartsWith(apiCustomFieldsErr)))).Returns(new RestResponse<CustomFields>() {Data = new CustomFields()});

            //CUSTOMERS
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerGood))).Returns(new RestResponse<Customer>() {StatusCode  = HttpStatusCode.OK, Data = new Customer{customer_id = "foo"} });
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerErr))).Returns(new RestResponse<Customer> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute<Customer>(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerErr))).Returns(new RestResponse<Customer>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute<Customer>(A<IRestRequest>.That.Not.Matches(r => r.Resource == apiCustomerErr))).Returns(new RestResponse<Customer>() { Data = new Customer{ customer_id = "foo"}});
            
            //CARRIERS
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierGood))).Returns(new RestResponse<Carrier>() { StatusCode = HttpStatusCode.OK, Data = new Carrier { carrier_id = "foo" } });
            A.CallTo(() => restClient.Execute(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierErr))).Returns(new RestResponse<Carrier> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute<Carrier>(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierErr))).Returns(new RestResponse<Carrier>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test")});
            A.CallTo(() => restClient.Execute<Carrier>(A<IRestRequest>.That.Not.Matches(r=>r.Resource==apiCarrierErr))).Returns(new RestResponse<Carrier>(){Data = new Carrier{carrier_id = "foo"}});

            //ADDRESSES
            A.CallTo(() => restClient.Execute<Address>(A<IRestRequest>.That.Matches(r => r.Resource == apiGetAddressError)))
                .Returns(new RestResponse<Address>{StatusCode = HttpStatusCode.InternalServerError});
            A.CallTo(() => restClient.Execute<Address>(A<IRestRequest>.That.Matches(r => r.Resource == apiGetAddressGood))).Returns(new RestResponse<Address>{StatusCode = HttpStatusCode.OK, Data = new Address()});

#if ASYNC
            
            //SHIPMENTS
            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiShipStatusErr )))
                .Returns(Task.FromResult(new RestResponse<Shipment> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Shipment>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiLockShipmentErr)))
                .Returns(Task.FromResult(new RestResponse<Shipment> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Shipment>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiShipStatusGood)))
                .Returns(Task.FromResult(new RestResponse<Shipment> { Data = new Shipment() } as IRestResponse<Shipment>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiLockShipmentGood )))
                .Returns(Task.FromResult(new RestResponse<Shipment> { Data = new Shipment() } as IRestResponse<Shipment>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Not.Matches(r=>r.Resource == apiShipmentErr)))
                .Returns(Task.FromResult(new RestResponse<Shipment>{Data = new Shipment() } as IRestResponse<Shipment>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Shipment>(A<IRestRequest>.That.Matches(r => r.Resource == apiShipmentErr)))
                .Returns(Task.FromResult(new RestResponse<Shipment>{StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Shipment>));

            //CustomFields
            A.CallTo(() => restClient.ExecuteTaskAsync<CustomFields>(A<IRestRequest>.That.Not.Matches(r => r.Resource.StartsWith(apiCustomFieldsErr))))
                .Returns(Task.FromResult(new RestResponse<CustomFields>() {Data = new CustomFields() } as IRestResponse<CustomFields>));

            //CUSTOMERS
            A.CallTo(() => restClient.ExecuteTaskAsync<Customer>(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerGood)))
                .Returns(Task.FromResult(new RestResponse<Customer>() {StatusCode  = HttpStatusCode.OK, Data = new Customer{customer_id = "foo"} } as IRestResponse<Customer>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Customer>(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerErr)))
                .Returns(Task.FromResult(new RestResponse<Customer> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Customer>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Customer>(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerErr)))
                .Returns(Task.FromResult(new RestResponse<Customer>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Customer>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Customer>(A<IRestRequest>.That.Not.Matches(r => r.Resource == apiCustomerErr)))
                .Returns(Task.FromResult(new RestResponse<Customer>() { Data = new Customer{ customer_id = "foo"} } as IRestResponse<Customer>));
            
            //CARRIERS
            A.CallTo(() => restClient.ExecuteTaskAsync<Carrier>(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierGood)))
                .Returns(Task.FromResult(new RestResponse<Carrier>() { StatusCode = HttpStatusCode.OK, Data = new Carrier { carrier_id = "foo" } } as IRestResponse<Carrier>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Carrier>(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierErr)))
                .Returns(Task.FromResult(new RestResponse<Carrier> { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Carrier>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Carrier>(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierErr)))
                .Returns(Task.FromResult(new RestResponse<Carrier>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") } as IRestResponse<Carrier>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Carrier>(A<IRestRequest>.That.Not.Matches(r=>r.Resource==apiCarrierErr)))
                .Returns(Task.FromResult(new RestResponse<Carrier>(){Data = new Carrier{carrier_id = "foo"} } as IRestResponse<Carrier>));

            //ADDRESSES
            A.CallTo(() => restClient.ExecuteTaskAsync<Address>(A<IRestRequest>.That.Matches(r => r.Resource == apiGetAddressError)))
                .Returns(Task.FromResult(new RestResponse<Address>{StatusCode = HttpStatusCode.InternalServerError } as IRestResponse<Address>));

            A.CallTo(() => restClient.ExecuteTaskAsync<Address>(A<IRestRequest>.That.Matches(r => r.Resource == apiGetAddressGood)))
                .Returns(Task.FromResult(new RestResponse<Address>{StatusCode = HttpStatusCode.OK, Data = new Address() } as IRestResponse<Address>));
            
#endif

            return restClient; 
        }

        public IRestClient GetBaseRestClient()
        {
            return base.GetRestClient();
        }
    }
}
