using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Business.Tests.Helpers;
using System.Linq;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ResultsProcessor_Tests
    {
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
    }
}

