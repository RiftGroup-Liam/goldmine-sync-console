using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Business;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System.Linq;
using System.Collections.Generic;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ChangeTrackingProcess_Tests
    {
        ChangeTrackingProcess _changeTrackingProcess;
        string _testAccountno;
        int _contact1StartVersionNumber;
        int _contact2StartVersionNumber;
        int _contsupStartVersionNumber;

        public ChangeTrackingProcess_Tests()
        {
            _changeTrackingProcess = new ChangeTrackingProcess();
            _testAccountno = "B5040182438 F[6CPLia";
        }

        [TestInitialize]
        public void DoInit()
        {
            _contact1StartVersionNumber = GetContact1StartVersionNumber();
            _contact2StartVersionNumber = GetContact2StartVersionNumber();
            _contsupStartVersionNumber = GetContSuppStartVersionNumber();
            CreateContact1Change();
            CreateContact2Change();
            CreateContSuppChange();
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

        // Helpers
        #region Helpers

        private void CreateContSuppChange()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContsuppChange(_testAccountno);
                if (result <= 0)
                {
                    throw new Exception("Can't create contsupp change");
                }
            }
        }
        private void CreateContact2Change()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContact2Change(_testAccountno);
                if (result <= 0)
                {
                    throw new Exception("Can't create contact2 change");
                }
            }
        }
        private void CreateContact1Change()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                int result = context.TESTS_CreateContact1Change(_testAccountno);
                if(result <= 0)
                {
                    throw new Exception("Can't create contact1 change");
                }
            }
        }

        private int GetContact1StartVersionNumber()
        {
            int contact1StartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contact1StartVersionNumber = int.Parse((from c in context.CONTACT1ChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch(Exception) { return contact1StartVersionNumber;  }
            return contact1StartVersionNumber;
        }
        private int GetContSuppStartVersionNumber()
        {
            int contSuppStartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contSuppStartVersionNumber = int.Parse((from c in context.CONTSUPPChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch (Exception) { return contSuppStartVersionNumber;  }
            return contSuppStartVersionNumber;
        }
        private int GetContact2StartVersionNumber()
        {
            int contact2StartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contact2StartVersionNumber = int.Parse((from c in context.CONTACT2ChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch (Exception) { return contact2StartVersionNumber; }
            return contact2StartVersionNumber;
        }

        #endregion
    }
}
