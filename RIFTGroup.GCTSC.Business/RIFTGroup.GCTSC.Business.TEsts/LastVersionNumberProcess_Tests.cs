using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Business.Tests.Helpers;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class LastVersionNumberProcess_Tests
    {
        int _contact1StartVersionNumber;
        int _contact2StartVersionNumber;
        int _contsuppStartVersionNumber;
        LastVersionNumberHelper _lastVersionNumberHelper;
        VersionNumberProcess _versionNumberProcess;
        string _testAccountno;
        CreateChangeHelper _createChangeHelper;
        ChangeTrackingProcess _changeTrackingProcess;

        public LastVersionNumberProcess_Tests()
        {
            _lastVersionNumberHelper = new LastVersionNumberHelper();
            _contact1StartVersionNumber = _lastVersionNumberHelper.Contact1StartVersionNumber;
            _contact2StartVersionNumber = _lastVersionNumberHelper.Contact2StartVersionNumber;
            _contsuppStartVersionNumber = _lastVersionNumberHelper.ContsuppStartVersionsNumber;
            _versionNumberProcess = new VersionNumberProcess();
            _testAccountno = "B5040182438 F[6CPLia";
            _createChangeHelper = new CreateChangeHelper(_testAccountno);
            _changeTrackingProcess = new ChangeTrackingProcess();
        }

        [TestMethod]
        public void Contact1_UpdatesLastVersionNumber()
        {
            _createChangeHelper.CreateContact1Change();
            _changeTrackingProcess.GetContact1ChangeTrackingResults(_contact1StartVersionNumber);
            Assert.IsTrue(_versionNumberProcess.GetCurrentContact1Version() != _contact1StartVersionNumber);
        }

        [TestMethod]
        public void Contact2_UpdatesLastVersionNumber()
        {
            _createChangeHelper.CreateContact2Change();
            _changeTrackingProcess.GetContact2ChangeTrackingResults(_contact2StartVersionNumber);
            Assert.IsTrue(_versionNumberProcess.GetCurrentContact2Version() != _contact2StartVersionNumber);
        }

        [TestMethod]
        public void Contsupp_UpdatesLastVersionNumber()
        {
            _createChangeHelper.CreateContSuppChange();
            _changeTrackingProcess.GetContSuppChangeTrackingResults(_contsuppStartVersionNumber);
            Assert.IsTrue(_versionNumberProcess.GetCurrentContsuppVersion() != _contsuppStartVersionNumber);
        }
    }
}
