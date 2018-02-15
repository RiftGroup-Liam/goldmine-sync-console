using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Business.Tests.Helpers;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class RequestProcessor_Tests
    {
        DataAPIClient _dataAPIClient;
        RequestProcessor _requestProcessor;
        ClientData _testClientData;
        ClientDataHelper _clientDataHelper;
        public RequestProcessor_Tests()
        {
            _requestProcessor = new RequestProcessor();
            _clientDataHelper = new ClientDataHelper();
            _dataAPIClient = new DataAPIClient();
        }

        [TestInitialize]
        public void DoInit()
        {
            do { _testClientData = _clientDataHelper.TestData; } while (ReferenceNumberExists(_testClientData.Key5));
        }

        [TestMethod]
        public void ProcessCreatePersonRequest_ReturnsResultObject()
        {
            ResultsObject result = _requestProcessor.ProcessCreatePersonRequest(_testClientData);
            Assert.IsTrue(result.Accountno == _testClientData.Accountno);
            Assert.IsTrue(result.Responses.Count == 5);
            Assert.IsTrue(result.Responses[0].URL.Contains("/people/"));
            Assert.IsTrue(result.Responses[4].URL.Contains("/person/phone_numbers"));
            Assert.IsTrue(result.Responses[2].URL.Contains("/person/email_addresses"));
            Assert.IsTrue(result.Responses[0].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(result.Responses[4].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(result.Responses[2].SendResponse == Core.Enums.Enums.SendResponse.OK);
        }

        private bool ReferenceNumberExists(string referenceNumber)
        {
            string personId = _dataAPIClient.GetPersonId(referenceNumber);
            if(string.IsNullOrEmpty(personId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
