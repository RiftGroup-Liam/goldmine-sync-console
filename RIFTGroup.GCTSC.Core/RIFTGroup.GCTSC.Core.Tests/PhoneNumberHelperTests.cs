using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core.Helpers;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class PhoneNumberHelperTests
    {
        string _expectedSubscriberNumber;
        public PhoneNumberHelperTests()
        {
            _expectedSubscriberNumber = "7887495880";
        }
        [TestMethod]
        public void CreateSubscriberNumber_FromOriginalWithSpace()
        {
            string testNumber = "07887 495880";
            string actualSubscriberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            Assert.IsTrue(actualSubscriberNumber == _expectedSubscriberNumber);
        }

        [TestMethod]
        public void CreateSubscriberNumber_FromOriginalWithText()
        {
            string testNumber = "07887 495880 - Liam";
            string actualSubscriberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            Assert.IsTrue(actualSubscriberNumber == _expectedSubscriberNumber);
        }

        [TestMethod]
        public void CreateSubscriberNumber_FromOriginalWithCountryCode()
        {
            string testNumber = "447887495880";
            string actualSubscriberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            Assert.IsTrue(actualSubscriberNumber == _expectedSubscriberNumber);
        }

        [TestMethod]
        public void CreateSubscriberNumber_FromOriginalWithPlusSign()
        {
            string testNumber = "+447887495880";
            string actualSubscriberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            Assert.IsTrue(actualSubscriberNumber == _expectedSubscriberNumber);
        }

        [TestMethod]
        public void CreateSubscriberNumber_DoesntErrorWithNullPhoneNumber()
        {
            string testNumber = null;
            string actualSubsciberNumber = null;
            try
            {
                actualSubsciberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            }
            catch(Exception ex) { Assert.Fail(ex.Message); }
            Assert.IsTrue(actualSubsciberNumber == string.Empty);
        }

        [TestMethod]
        public void CreateSubscriberNumber_DoesntErrorWithEmptyPhoneNumber()
        {
            string testNumber = "";
            string actualSubsciberNumber = null;
            try
            {
                actualSubsciberNumber = PhoneNumberHelper.CreateSubscriberNumber(testNumber);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
            Assert.IsTrue(actualSubsciberNumber == string.Empty);
        }
    }
}
