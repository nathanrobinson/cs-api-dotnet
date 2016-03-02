using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using cs_api_dotnet;
using NUnit.Framework;

namespace cs_api_dotnet_tests
{
    [TestFixture]
    public class CaseStackAPITests
    {
        private CaseStackApi _api;

        [SetUp]
        public void Setup()
        {
            _api = new CaseStackApi();
        }

        #region Constructor Tests

        [Test]
        public void Construct_CaseStackAPi_Url_Is_Production()
        {
            _api = new CaseStackApi(true);
            Assert.True(_api.ApiEndpoint == "https://staging.casestack.io");
        }

        [Test]
        public void Construct_CaseStackApi_Url_Is_Staging()
        {
            Assert.True(_api.ApiEndpoint == "https://app.casestack.io");
        }

        #endregion

        #region Authentication Tests

        [Test]
        public void Authenticate_Key_Is_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.Authenticate(null, "foo"));
        }

        [Test]
        public void Authenticate_Key_Is_Empty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.Authenticate(string.Empty, "foo"));
        }

        [Test]
        public void Authenticate_Company_Is_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.Authenticate("foo", null));
        }

        [Test]
        public void Authenticate_Company_Is_Empty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.Authenticate("foo", string.Empty));
        }

        [Test]
        public void Authenticate_Does_Not_Throw_Exception()
        {
            _api.Authenticate("foo", "foo");
        }

        #endregion

        #region Carrier Tests

        [Test]
        public void GetCarrier_CarrierId_Empty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.GetCarrier(string.Empty));
        }

        [Test]
        public void GetCarrier_CarrierId_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.GetCarrier(null));
        }

        [Test]
        public void GetCarrier_Data_Valid()
        {
            var api = new CaseStackApiStub();

            api.Authenticate("foo", "foo");
            var carrier = api.GetCarrier("foo");
            Assert.IsNotNull(carrier);
        }

        [Test]
        public void GetCarrier_Throws_HttpException()
        {
            var api = new CaseStackApiStub();

            api.Authenticate("foo", "foo");
            var exception = Assert.Throws<HttpException>(() => api.GetCarrier("err"));
            Assert.True(500 == exception.GetHttpCode());
        }

        #endregion

        #region Customer Tests

        [Test]
        public void GetCustomer_CustomerId_Empty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.GetCustomer(string.Empty));
        }

        [Test]
        public void GetCustomer_CustomerId_Null_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _api.GetCustomer(null));
        }

        [Test]
        public void GetCustomer_Customer_NotNull()
        {
            var api = new CaseStackApiStub();

            api.Authenticate("foo", "foo");
            var carrier = api.GetCustomer("foo");
            Assert.IsNotNull(carrier);
        }

        [Test]
        public void GetCustomer_Throws_HttpException()
        {
            var api = new CaseStackApiStub();

            api.Authenticate("foo", "foo");
            var exception = Assert.Throws<HttpException>(() => api.GetCustomer("err"));
            Assert.True(500 == exception.GetHttpCode());
        }

        #endregion

        #region CustomField Tests

        [Test]
        public void GetCustomFields_Carrier_No_Errors()
        {
            _api.GetCustomFields<Carrier>();
        }

        #endregion

    }
}

