using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Business.Tests.Helpers;
using System.Linq;
using RIFTGroup.GCTSC.Core.EntityFramework;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ResultsProcessor_Tests
    {
        DataAPIClient _dataApiClient;
        ResultsProcessor _resultsProcessor;
        List<CONTACT1ChangeTracking_Result> _contact1Results;
        List<CONTACT2ChangeTracking_Result> _contact2Results;
        List<CONTSUPPChangeTracking_Result> _contsuppResults;
        string _testAccountno;
        int _contact1StartVersionNumber;
        int _contact2StartVersionNumber;
        int _contsuppStartVersionNumber;
        LastVersionNumberHelper _lastVersionNumberHelper;
        CreateChangeHelper _createChangeHelper;
        ChangeTrackingProcess _changeTrackingProcess;
        public ResultsProcessor_Tests()
        {
            _lastVersionNumberHelper = new LastVersionNumberHelper();
            _contact1StartVersionNumber = _lastVersionNumberHelper.Contact1StartVersionNumber;
            _contact2StartVersionNumber = _lastVersionNumberHelper.Contact2StartVersionNumber;
            _contsuppStartVersionNumber = _lastVersionNumberHelper.ContsuppStartVersionsNumber;
            _resultsProcessor = new ResultsProcessor();
            _testAccountno = "B5040182438 F[6CPLia";
            _createChangeHelper = new CreateChangeHelper(_testAccountno);
            _changeTrackingProcess = new ChangeTrackingProcess();
            _dataApiClient = new DataAPIClient();
        }

        [TestInitialize]
        public void DoInit()
        {
            _createChangeHelper.CreateContact1Change();
            _createChangeHelper.CreateContact2Change();
            _createChangeHelper.CreateContSuppChange();
            _contact1Results = _changeTrackingProcess.GetContact1ChangeTrackingResults(_contact1StartVersionNumber);
            _contact2Results = _changeTrackingProcess.GetContact2ChangeTrackingResults(_contact2StartVersionNumber);
            _contsuppResults = _changeTrackingProcess.GetContSuppChangeTrackingResults(_contsuppStartVersionNumber);
        }

        [TestMethod]
        public void GivesBackResultsOfAPICalls()
        {
            List<ResultsObject> results = _resultsProcessor.ProcessAPICalls(_contact1Results, _contact2Results, _contsuppResults);
            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(results[0].Responses.Where(x => x.SendRequest == Core.Enums.Enums.SendRequest.PHONE1).Any());
            Assert.IsTrue(results[1].Responses.Where(x => x.SendRequest == Core.Enums.Enums.SendRequest.USTAGE1DAT).Any());
            Assert.IsTrue(results[2].Responses.Where(x => x.SendRequest == Core.Enums.Enums.SendRequest.CONTSUPREF).Any());
        }

        [TestMethod]
        public void SendContact1Changes_WhenPersonDoesNotExist()
        {
            CreateContact1Change();
        }

        [TestMethod]
        public void SendContsuppChanges_WhenPersonDoesNotExist()
        {
            CreateContsuppChange();
        }

        [TestMethod]
        public void SendContact2Changes_WhenPersonDoesNotExist()
        {
            CreateContact2Change();
        }

        private void CreateContact1Change()
        {
            string accountno = string.Empty;
            string randomReference = string.Empty;

            do
            {
                randomReference = CreateRandomReference();
            } while (ReferenceNumberExists(randomReference));

            accountno = GetAccountnoFromKey5(randomReference);
            List<CONTACT1ChangeTracking_Result> results = new List<CONTACT1ChangeTracking_Result>();
            CONTACT1ChangeTracking_Result result = new CONTACT1ChangeTracking_Result() { contactChanged = 1, ACCOUNTNO = GetAccountnoFromKey5(randomReference) };
            results.Add(result);

            List<ResultsObject> ros = _resultsProcessor.ProcessAPICalls(results, new List<CONTACT2ChangeTracking_Result>(), new List<CONTSUPPChangeTracking_Result>());
            Assert.IsTrue(ros[0].Responses.Count == 5);
            Assert.IsTrue(ros[0].Responses[0].URL.Contains("/people/"));
            Assert.IsTrue(ros[0].Responses[4].URL.Contains("/person/phone_numbers"));
            Assert.IsTrue(ros[0].Responses[2].URL.Contains("/person/email_addresses"));
            Assert.IsTrue(ros[0].Responses[0].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[4].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[2].SendResponse == Core.Enums.Enums.SendResponse.OK);
        }

        private void CreateContact2Change()
        {
            string accountno = string.Empty;
            string randomReference = string.Empty;

            do
            {
                randomReference = CreateRandomReference();
            } while (ReferenceNumberExists(randomReference));

            accountno = GetAccountnoFromKey5(randomReference);
            List<CONTACT2ChangeTracking_Result> results = new List<CONTACT2ChangeTracking_Result>();
            CONTACT2ChangeTracking_Result result = new CONTACT2ChangeTracking_Result() { uemailaddrChanged = 1, ACCOUNTNO = GetAccountnoFromKey5(randomReference) };
            results.Add(result);

            List<ResultsObject> ros = _resultsProcessor.ProcessAPICalls(new List<CONTACT1ChangeTracking_Result>(), results, new List<CONTSUPPChangeTracking_Result>());
            Assert.IsTrue(ros[0].Responses.Count == 5);
            Assert.IsTrue(ros[0].Responses[0].URL.Contains("/people/"));
            Assert.IsTrue(ros[0].Responses[4].URL.Contains("/person/phone_numbers"));
            Assert.IsTrue(ros[0].Responses[2].URL.Contains("/person/email_addresses"));
            Assert.IsTrue(ros[0].Responses[0].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[4].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[2].SendResponse == Core.Enums.Enums.SendResponse.OK);
        }

        private void CreateContsuppChange()
        {
            string recid = string.Empty;
            string randomReference = string.Empty;

            do
            {
                randomReference = CreateRandomReference();
                recid = GetEmailAddrRecId(randomReference);
            } while (ReferenceNumberExists(randomReference) || recid == string.Empty);
            
            List<CONTSUPPChangeTracking_Result> results = new List<CONTSUPPChangeTracking_Result>();
            CONTSUPPChangeTracking_Result result = new CONTSUPPChangeTracking_Result() { contsuprefChanged = 1, recid = recid };
            results.Add(result);

            List<ResultsObject> ros = _resultsProcessor.ProcessAPICalls(new List<CONTACT1ChangeTracking_Result>(), new List<CONTACT2ChangeTracking_Result>(), results);
            Assert.IsTrue(ros[0].Responses.Count == 5);
            Assert.IsTrue(ros[0].Responses[0].URL.Contains("/people/"));
            Assert.IsTrue(ros[0].Responses[4].URL.Contains("/person/phone_numbers"));
            Assert.IsTrue(ros[0].Responses[2].URL.Contains("/person/email_addresses"));
            Assert.IsTrue(ros[0].Responses[0].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[4].SendResponse == Core.Enums.Enums.SendResponse.OK);
            Assert.IsTrue(ros[0].Responses[2].SendResponse == Core.Enums.Enums.SendResponse.OK);
        }
        private string CreateRandomReference()
        {
            Random rnd = new Random();
            return rnd.Next(1, 333333).ToString(); ;
        }

        private string GetEmailAddrRecId(string randomReference)
        {
            string accountno = string.Empty;
            string emailRecid = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                accountno = (from c in context.CONTACT1.Where(c => c.KEY5 == randomReference) select c.ACCOUNTNO).FirstOrDefault();
                if (accountno != string.Empty)
                {
                    emailRecid = (from cs in context.CONTSUPPs.Where(cs => cs.ACCOUNTNO == accountno && cs.RECTYPE == "P" && cs.ZIP == "011") select cs.recid).FirstOrDefault();
                }
            }
            return emailRecid;
        }

        private string GetAccountnoFromKey5(string randomReference)
        {
            string accountno = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                accountno = (from c in context.CONTACT1.Where(c => c.KEY5 == randomReference) select c.ACCOUNTNO).FirstOrDefault();
            }
            return accountno;
        }

        private bool ReferenceNumberExists(string referenceNumber)
        {
            string personId = _dataApiClient.GetPersonId(referenceNumber);
            if (string.IsNullOrEmpty(personId))
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

