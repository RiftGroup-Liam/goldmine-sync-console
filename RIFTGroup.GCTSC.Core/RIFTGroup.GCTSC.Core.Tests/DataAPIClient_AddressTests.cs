using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core.Model;
using System.Collections.Generic;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class DataAPIClient_AddressTests
    {
        DataAPIClient _dataApiClient;
        string _testFirstLine;
        string _testPostcode;
        string _testAddressId;
        string _testPersonId;
        Address _testAddress;
        Random _random;
        ResultsObject _testResultsObject;
        public DataAPIClient_AddressTests()
        {
            _dataApiClient = new DataAPIClient();
            _testFirstLine = "93 Rectory Road";
            _testPostcode = "ct149nb";
            _testAddressId = "5";
            _testPersonId = "4185";
            _random = new Random();
            _testAddress = CreateTestAddress();
            _testResultsObject = new ResultsObject()
            {
                ReferenceNumber = "165992",
                Accountno = "B5040182438 F[6CPLia"
            };
            _testResultsObject.Responses = new List<ResponseDetails>();
        }

        private Address CreateTestAddress()
        {
            return new Address()
            {
                first_line = string.Format("{0} the bungalow", _random.Next().ToString()),
                second_line = string.Format("{0} testing road", _random.Next().ToString()),
                country = "England",
                county = "Kent",
                postcode = "AA157BB",
                town = "Town"
            };
        }

        [TestMethod]
        public void ChecksAnAddressExists()
        {
            string addressId = _dataApiClient.GetExistingAddress(_testFirstLine, _testPostcode);
            Assert.IsTrue(!string.IsNullOrEmpty(addressId));
        }

        [TestMethod]
        public void CheckForOthersLinkedToThisAddress()
        {
            bool otherLinkedToThisAddress = _dataApiClient.CheckForOthersLinkedToThisAddress(_testAddressId);
            Assert.IsTrue(otherLinkedToThisAddress);
        }

        [TestMethod]
        public void CheckForOtherActiveAddresses()
        {
            bool otherActiveAddresses = _dataApiClient.CheckForOtherActiveAddresses(_testPersonId);
            Assert.IsTrue(otherActiveAddresses);
        }

        [TestMethod]
        public void CreateAddress()
        {
            string newAddressId = _dataApiClient.CreateAddress(_testAddress, _testResultsObject);
            Assert.IsTrue(!string.IsNullOrEmpty(newAddressId));
        }

        [TestMethod]
        public void CreatePersonAddress()
        {
            string personAddressId = _dataApiClient.CreatePersonAddress(_testAddressId, _testPersonId, _testResultsObject);
            Assert.IsTrue(!string.IsNullOrEmpty(personAddressId));
        }
    }
}
