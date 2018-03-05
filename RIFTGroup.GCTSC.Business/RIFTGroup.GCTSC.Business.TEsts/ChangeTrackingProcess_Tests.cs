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
        CreateChangeHelper _createNonBAUChangeHelper;
        string _testAccountno;
        string _nonBAUTestAccountno;
        int _contact1StartVersionNumber;
        int _contact2StartVersionNumber;
        int _contsupStartVersionNumber;

        public ChangeTrackingProcess_Tests()
        {
            _changeTrackingProcess = new ChangeTrackingProcess();
            _testAccountno = "B5040182438 F[6CPLia";
            _nonBAUTestAccountno = "B5072357991&1RR@!Pet";
            _lastVersionNumberHelper = new LastVersionNumberHelper();
            _createChangeHelper = new CreateChangeHelper(_testAccountno);
            _createNonBAUChangeHelper = new CreateChangeHelper(_nonBAUTestAccountno);
        }

        [TestInitialize]
        public void DoInit()
        {
            _contact1StartVersionNumber = _lastVersionNumberHelper.Contact1StartVersionNumber;
            _contact2StartVersionNumber = _lastVersionNumberHelper.Contact2StartVersionNumber;
            _contsupStartVersionNumber = _lastVersionNumberHelper.ContsuppStartVersionsNumber;
            CreateNonBAUCustomer(_nonBAUTestAccountno);
            _createChangeHelper.CreateContact1Change();
            _createChangeHelper.CreateContact2Change();
            _createChangeHelper.CreateContSuppChange();
            _createNonBAUChangeHelper.CreateContact1Change();
            _createNonBAUChangeHelper.CreateContact2Change();
            _createNonBAUChangeHelper.CreateContSuppChange();
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

        [TestMethod]
        public void CONTACT1ChangeTracking_DoesntReturnResults_ForNonBAUCustomer()
        {
            List<CONTACT1ChangeTracking_Result> results = _changeTrackingProcess.GetContact1ChangeTrackingResults(_contact1StartVersionNumber);
            foreach(CONTACT1ChangeTracking_Result result in results)
            {
                Assert.IsTrue(result.rectype == "BAU");
            }
        }

        [TestMethod]
        public void CONTACT2ChangeTracking_DoesntReturnResults_ForNonBAUCustomer()
        {
            List<CONTACT2ChangeTracking_Result> results = _changeTrackingProcess.GetContact2ChangeTrackingResults(_contact2StartVersionNumber);
            foreach (CONTACT2ChangeTracking_Result result in results)
            {
                Assert.IsTrue(result.RECTYPE == "BAU");
            }
        }

        [TestMethod]
        public void CONTSUPPChangeTracking_DoesntReturnResults_ForNonBAUCustomer()
        {
            List<CONTSUPPChangeTracking_Result> results = _changeTrackingProcess.GetContSuppChangeTrackingResults(_contsupStartVersionNumber);
            foreach (CONTSUPPChangeTracking_Result result in results)
            {
                Assert.IsTrue(result.RecType == "BAU");
            }
        }

        private void CreateNonBAUCustomer(string _nonBAUTestAccountno)
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                context.TESTS_CreateNonBAUCustomer(_nonBAUTestAccountno);
            }
        }
    }
}
