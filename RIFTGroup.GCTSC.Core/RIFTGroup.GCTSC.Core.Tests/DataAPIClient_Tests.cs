using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RestSharp;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class DataAPIClient_Tests
    {
        DataAPIClient _restClient;
        string _testReference;
        string _testAccountno;
        AppSettings _appSettings;
        string _dataAPITestPersonId; // TODO: Cleanup as this is reliance on my existing record in the Data API
        public DataAPIClient_Tests()
        {
            _appSettings = new AppSettings();
            _restClient = new DataAPIClient();
            _testReference = "165992";
            _testAccountno = "B5040182438 F[6CPLia";
            _dataAPITestPersonId = "1";
        }

        [TestMethod]
        public void SendsUpdatePersonRequest_OKResponse()
        {
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
                ChangedValue = "TestChange"
            };
            ro = _restClient.SendUpdatePersonRequest(Enums.Enums.SendRequest.SECR, ro);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[0].URL.Contains("/people/" + _dataAPITestPersonId));
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdateEmailRequest_OKResponse()
        {
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
                ChangedValue = "test_change@riftgroup.com"
            };
            ro = _restClient.SendUpdateEmailAddressRequest(Enums.Enums.SendRequest.CONTSUPREF, ro);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[1].URL.Contains("/person/email_addresses/"));
            Assert.IsTrue(ro.Responses[1].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdatePhoneNumberRequest_OKResponse()
        {
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
                ChangedValue = "07887495880"
            };
            ro = _restClient.SendUpdatePhoneNumberRequest(Enums.Enums.SendRequest.PHONE1, ro);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[0].URL.Contains("/people/phone_numbers/"));
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }
    }
}
