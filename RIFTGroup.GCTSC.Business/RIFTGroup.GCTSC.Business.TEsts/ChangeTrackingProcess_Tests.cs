using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Business;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System.Linq;
using System.Collections.Generic;
using RIFTGroup.GCTSC.Business.Tests.Helpers;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ChangeTrackingProcess_Tests
    {
        ChangeTrackingProcess _changeTrackingProcess;
        LastVersionNumberHelper _lastVersionNumberHelper;
        CreateChangeHelper _createChangeHelper;
        string _testAccountno;
        int _contact1StartVersionNumber;
        int _contact2StartVersionNumber;
        int _contsupStartVersionNumber;

        public ChangeTrackingProcess_Tests()
        {
            _changeTrackingProcess = new ChangeTrackingProcess();
            _testAccountno = "B5040182438 F[6CPLia";
            _lastVersionNumberHelper = new LastVersionNumberHelper();
            _createChangeHelper = new CreateChangeHelper(_testAccountno);
        }

        [TestInitialize]
        public void DoInit()
        {
            _contact1StartVersionNumber = _lastVersionNumberHelper.Contact1StartVersionNumber;
            _contact2StartVersionNumber = _lastVersionNumberHelper.Contact2StartVersionNumber;
            _contsupStartVersionNumber = _lastVersionNumberHelper.ContsuppStartVersionsNumber;
            _createChangeHelper.CreateContact1Change();
            _createChangeHelper.CreateContact2Change();
            _createChangeHelper.CreateContSuppChange();
        }

        [TestMethod]
        public void CONTACT1ChangeTracking_ReturnsResults()
        {
            List<CONTACT1ChangeTracking_Result> results = _changeTrackingProcess.GetContact1ChangeTrackingResults(_contact1StartVersionNumber);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CONTACT2ChangeTracking_ReturnsResults()
        {
            List<CONTACT2ChangeTracking_Result> results = _changeTrackingProcess.GetContact2ChangeTrackingResults(_contact2StartVersionNumber);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CONTSUPPChangeTracking_ReturnsResults()
        {
            List<CONTSUPPChangeTracking_Result> results = _changeTrackingProcess.GetContSuppChangeTrackingResults(_contsupStartVersionNumber);
            Assert.IsTrue(results.Count > 0);
        }
    }
}
