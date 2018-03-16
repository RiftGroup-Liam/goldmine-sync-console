using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class GMRepo_Tests
    {
        string _testRecid;
        string _testAccountno;
        string _testReferenceNumber;
        ResultsObject _testRo;
        GM_Repository _gmRepo;
        public GMRepo_Tests()
        {
            _gmRepo = new GM_Repository();
            _testRecid = "IAUWSAI$U3J8P9*";
            _testAccountno = "B5040182438 F[6CPLia";
            _testRo = new ResultsObject();
            _testReferenceNumber = "165992";
        }

        [TestInitialize]
        public void DoInit()
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                context.Tests_GCTSC_UpdateGMRepoTestData();
            }
        }

        [TestMethod]
        public void GetReferenceNumberFromRecid()
        {
            string expected = _testReferenceNumber;
            ResultsObject actual = new ResultsObject();
            actual = _gmRepo.GetReferenceNumberFromRecid(_testRecid, _testRo);
            Assert.AreEqual(expected, actual.ReferenceNumber);
        }

        [TestMethod]
        public void GetAccountnoFromRecid()
        {
            string expected = _testAccountno;
            ResultsObject actual = new ResultsObject();
            actual = _gmRepo.GetAccountnoFromRecid(_testRecid, _testRo);
            Assert.AreEqual(expected, actual.Accountno);
        }

        [TestMethod]
        public void GetReferenceNumberFromAccountno()
        {
            string expected = _testReferenceNumber;
            ResultsObject actual = new ResultsObject();
            actual = _gmRepo.GetReferenceNumberFromAccountno(_testAccountno, _testRo);
            Assert.AreEqual(expected, actual.ReferenceNumber);
        }

        [TestMethod]
        public void GetContsupref_FromRecid()
        {
            string expected = "liamarnold93@gmail.com";
            string actual = _gmRepo.GetContsupref(_testRecid);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetKey5_FromAccoutno()
        {
            string expected = _testReferenceNumber;
            string actual = _gmRepo.GetKey5(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSecr_FromAccountno()
        {
            string expected = "Liam";
            string actual = _gmRepo.GetSecr(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetContact_FromAccountno()
        {
            string expected = "Liam Arnold";
            string actual = _gmRepo.GetContact(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetClientData_FromReferenceNumber()
        {
            ClientData expectedClientData = new ClientData()
            {
                Accountno = _testAccountno,
                Key5 = _testReferenceNumber,
                Lastname = "Arnold",
                Phone1 = "07887495880",
                Phone2 = "01304362644",
                Phone3 = "01227453980",
                Secr = "Liam",
                UconvDate = "14/02/2018 00:00:00",
                UStage1Dat = "13/02/2018 00:00:00",
                UEmailAddr = "larnold@riftgroup.com"
            };
            ClientData actualClientData = _gmRepo.GetClientData(_testReferenceNumber);
            Assert.AreEqual(expectedClientData.Accountno, actualClientData.Accountno);
            Assert.AreEqual(expectedClientData.Key5, actualClientData.Key5);
            Assert.AreEqual(expectedClientData.Lastname, actualClientData.Lastname);
            Assert.AreEqual(expectedClientData.Phone1, actualClientData.Phone1);
            Assert.AreEqual(expectedClientData.Phone2, actualClientData.Phone2);
            Assert.AreEqual(expectedClientData.Phone3, actualClientData.Phone3);
            Assert.AreEqual(expectedClientData.Secr, actualClientData.Secr);
            Assert.AreEqual(expectedClientData.UconvDate, actualClientData.UconvDate);
            Assert.AreEqual(expectedClientData.UStage1Dat, actualClientData.UStage1Dat);
            Assert.AreEqual(expectedClientData.UEmailAddr, actualClientData.UEmailAddr);
        }

        [TestMethod]
        public void GetLastname_FromAccountno()
        {
            string expected = "Arnold";
            string actual = _gmRepo.GetLastname(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPhone1_FromAccountno()
        {
            string expected = "07887495880";
            string actual = _gmRepo.GetPhone1(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPhone2_FromAccountno()
        {
            string expected = "01304362644";
            string actual = _gmRepo.GetPhone2(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPhone3_FromAccountno()
        {
            string expected = "01227453980";
            string actual = _gmRepo.GetPhone3(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUsatge1Dat_FromAccountno()
        {
            string expected = "13/02/2018 00:00:00";
            string actual = _gmRepo.GetUstage1dat(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUconvdate_FromAccountno()
        {
            string expected = "14/02/2018 00:00:00";
            string actual = _gmRepo.GetUconvdate(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetUemailAddr_FromAccountno()
        {
            string expected = "larnold@riftgroup.com";
            string actual = _gmRepo.GetUemailaddr(_testAccountno);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTranslatedCaseOwner_FromAccountno()
        {
            string expected = "larnold";
            string actual = _gmRepo.GetTranslatedCaseOwner(_testAccountno);
            Assert.AreEqual(expected, actual);
        }
    }
}

