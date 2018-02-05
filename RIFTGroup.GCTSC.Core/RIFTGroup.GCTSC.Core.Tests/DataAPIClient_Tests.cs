using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core;
using RestSharp;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class DataAPIClient_Tests
    {
        DataAPIClient _restClient;
        string _testReference;
        string _madeUpReference;
        AppSettings _appSettings;
        public DataAPIClient_Tests()
        {
            _appSettings = new AppSettings();
            _restClient = new DataAPIClient();
            _testReference = "165992";
            _madeUpReference = "#NotReal";
        }
        [TestMethod]
        public void GetsReferenceNumberFromAPI()
        {
            string personId = string.Empty;
            PrivateObject objToTest = new PrivateObject(typeof(DataAPIClient));
            personId = Convert.ToString(objToTest.Invoke("GetPersonId", _testReference));
            Assert.IsTrue(!string.IsNullOrEmpty(personId));
        }

        [TestMethod]
        public void GetsEmptyReferenceNumberFromAPI_WhenSupplyingMadeUpReferenceNumber()
        {
            string personId = string.Empty;
            PrivateObject objToTest = new PrivateObject(typeof(DataAPIClient));
            objToTest.SetFieldOrProperty("_restClient", new RestClient(_appSettings.ApiURL));
            personId = Convert.ToString(objToTest.Invoke("GetPersonId", _madeUpReference));
            Assert.IsTrue(string.IsNullOrEmpty(personId));
        }
    }
}
