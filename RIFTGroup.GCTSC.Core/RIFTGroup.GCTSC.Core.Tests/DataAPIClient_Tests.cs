﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RestSharp;
using System.Collections.Generic;
using RIFTGroup.GCTSC.Business.Tests.Helpers;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class DataAPIClient_Tests
    {
        private AppSettings _appSettings;
        private ClientDataHelper _clientDataHelper;
        private string _dataAPITestPersonId;
        private DataAPIClient _restClient;
        private string _testAccountno;
        private string _testReference;

        // TODO: Cleanup as this is reliance on my existing record in the Data API
        public DataAPIClient_Tests()
        {
            _appSettings = new AppSettings();
            _restClient = new DataAPIClient();
            _testReference = "165992";
            _testAccountno = "B5040182438 F[6CPLia";
            _dataAPITestPersonId = "1";
            _clientDataHelper = new ClientDataHelper();
        }

        [TestMethod]
        public void CreatePersonRequest_OKResponse()
        {
            ClientData clientData = _clientDataHelper.TestData;
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = clientData.Key5,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.CreatePersonRequest(ro, clientData);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.Responses[0].URL.Contains("/people/"));
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdateCommunicationPreference_OKResponse()
        {
            ClientData clientData = _clientDataHelper.TestData;
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = clientData.Key5,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdateCommunicationPreference(Enums.Enums.CommPreferenceType.Email, ro, false);
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdateEmailRequest_OKResponse()
        {
            string changedValue = "test_change@riftgroup.com";
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdateEmailAddressRequest(Enums.Enums.SendRequest.CONTSUPREF, ro, changedValue);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[1].URL.Contains("/person/email_addresses/"));
            Assert.IsTrue(ro.Responses[1].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdateOfClaimStatus_OKResponse()
        {
            ClientData clientData = _clientDataHelper.TestData;
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = clientData.Key5,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdateClaimStatus(Enums.Enums.Year.UY18, ro, 8);
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdatePersonRequest_OKResponse()
        {
            string changedValue = "TestChange";
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdatePersonRequest(Enums.Enums.SendRequest.SECR, ro, changedValue);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[0].URL.Contains("/people/" + _dataAPITestPersonId));
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void SendsUpdatePhoneNumberRequest_OKResponse()
        {
            string changedValue = "07887495880";
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = _testReference,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdatePhoneNumberRequest(Enums.Enums.SendRequest.PHONE1, ro, changedValue);
            Assert.IsTrue(ro.Accountno == _testAccountno);
            Assert.IsTrue(ro.ReferenceNumber == _testReference);
            Assert.IsTrue(ro.Responses[0].URL.Contains("/person/phone_numbers"));
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }

        [TestMethod]
        public void UpdateCaseOwner_OKResponse()
        {
            ClientData clientData = _clientDataHelper.TestData;
            ResultsObject ro = new ResultsObject()
            {
                Accountno = _testAccountno,
                ReferenceNumber = clientData.Key5,
            };
            ro.Responses = new List<ResponseDetails>();
            ro = _restClient.SendUpdateCaseOwnerRequest(Enums.Enums.SendRequest.UBCASEOWN, ro, "larnold");
            Assert.IsTrue(ro.Responses[0].SendResponse == Enums.Enums.SendResponse.OK);
        }
    }
}