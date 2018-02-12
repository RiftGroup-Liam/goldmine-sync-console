using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Business.Tests.Helpers;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System.Linq;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ApplicationLogging_Tests
    {
        ApplicationLogging _applicationLogging;
        ResultsObject _testRO;
        ResultsObjectTestDataHelper _roTestHelper;
        public ApplicationLogging_Tests()
        {
            _applicationLogging = new ApplicationLogging();
            _testRO = new ResultsObject();
            _roTestHelper = new ResultsObjectTestDataHelper();
            _testRO = _roTestHelper.CreateTestData();            
        }

        [TestInitialize]
        public void DoInit()
        {
            ClearTestData();
            _applicationLogging.Log(_testRO, Core.Enums.Enums.SendRequest.LASTNAME);
        }

        [TestMethod]
        public void SuccessfullyCreatesChangeTrackingEvent()
        {            
            CheckForChangeTracking();
        }

        [TestMethod]
        public void SuccessfullyCreatesChangeTrackingEvent_Request()
        {
            CheckForChangeTrackingRequest();
        }

        [TestMethod]
        public void SuccessfullyCreatesChangeTrackingEvent_UpdateRequest()
        {
            CheckForChangeTrackingUpdateRequest();
        }

        private void CheckForChangeTrackingRequest()
        {
            using (RiftEntities context = new RiftEntities())
            {
                GCTSC_ChangeTracking ctRecord = (from ct in context.GCTSC_ChangeTracking.Where(ct => ct.Accountno == _roTestHelper._testAccountno)
                                                 select ct).FirstOrDefault();
                if (ctRecord == null) { throw new ArgumentNullException("ctRecord"); }
                else
                {
                    GCTSC_ChangeTracking_Requests ctrRecord = (from ctr in context.GCTSC_ChangeTracking_Requests.Where(
                                                                        ctr => ctr.ChangeTrackingId == ctRecord.Id
                                                                        && ctr.ResponseCode == "OK"
                                                                        )
                                                                      select ctr).FirstOrDefault();
                    Assert.IsTrue(ctrRecord != null);
                }
            }
        }

        private void CheckForChangeTracking()
        {
            using (RiftEntities context = new RiftEntities())
            {
                GCTSC_ChangeTracking ctRecord = (from ct in context.GCTSC_ChangeTracking.Where(ct => ct.Accountno == _roTestHelper._testAccountno)
                                                 select ct).FirstOrDefault();
                Assert.IsTrue(ctRecord != null);
            }
        }

        private void CheckForChangeTrackingUpdateRequest()
        {
            using (RiftEntities context = new RiftEntities())
            {
                GCTSC_ChangeTracking ctRecord = (from ct in context.GCTSC_ChangeTracking.Where(ct => ct.Accountno == _roTestHelper._testAccountno)
                                                 select ct).FirstOrDefault();
                if(ctRecord == null) { throw new ArgumentNullException("ctRecord"); }
                else
                {
                    GCTSC_ChangeTracking_UpdateRequests cturRecord = (from ctur in context.GCTSC_ChangeTracking_UpdateRequests.Where(
                                                                        ctur => ctur.ChangeTrackingId == ctRecord.Id
                                                                        && ctur.ResponseCode == "OK"
                                                                        && ctur.Change == "LASTNAME"
                                                                        && ctur.ResponseContent.Contains(@"{""id"":1,""first_name"":""TestChange"",""goldmine_customer_number"":165992")
                                                                        )
                                                                      select ctur).FirstOrDefault();
                    Assert.IsTrue(cturRecord != null);
                }
            }
        }

        private void ClearTestData()
        {
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.TESTs_ClearGCTSCChangeTrackingLogs();
                } catch(Exception ex) { throw ex;  }
            }
        }
    }
}
