using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using cs_api_dotnet;
using FakeItEasy;
using RestSharp;

namespace cs_api_dotnet_tests
{
    public class CaseStackApiStub : CaseStackApi
    {
        protected override IRestClient GetRestClient()
        {
            var restClient = A.Fake<IRestClient>();
            var apiCarrierErr = "api/carrier/err";
            var apiCustomerErr = "api/customer/err";

            A.CallTo(() => restClient.Execute<Customer>(A<IRestRequest>.That.Matches(r => r.Resource == apiCustomerErr))).Returns(new RestResponse<Customer>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test") });
            A.CallTo(() => restClient.Execute<Customer>(A<IRestRequest>.That.Not.Matches(r => r.Resource == apiCustomerErr))).Returns(new RestResponse<Customer>() { Data = new Customer() });
            
            A.CallTo(() => restClient.Execute<Carrier>(A<IRestRequest>.That.Matches(r => r.Resource == apiCarrierErr))).Returns(new RestResponse<Carrier>() { StatusCode = HttpStatusCode.InternalServerError, ErrorException = new Exception("test")});
            A.CallTo(() => restClient.Execute<Carrier>(A<IRestRequest>.That.Not.Matches(r=>r.Resource==apiCarrierErr))).Returns(new RestResponse<Carrier>(){Data = new Carrier()});
            return restClient; 
        }
    }
}
