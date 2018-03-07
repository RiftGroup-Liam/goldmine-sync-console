using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core.Helpers;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class NameHelperTests
    {
        string _value = "Liam John Arnold";
        string _noValue = string.Empty;

        [TestMethod]
        public void CreateFirstname_WithValue()
        {
            string expected = "Liam";
            string actual = NameHelper.CreateFirstnameFromContact(_value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateMiddleNames_WithValue()
        {
            string expected = "John";
            string actual = NameHelper.CreateMiddleNamesFromContact(_value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateLastname_WithValue()
        {
            string expected = "Arnold";
            string actual = NameHelper.CreateLastnameFromContact(_value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateFirstname_WithoutValue()
        {
            try
            {
                string actual = NameHelper.CreateFirstnameFromContact(_noValue);
                Assert.AreEqual(actual, string.Empty);
            }
            catch (Exception ex) { Assert.Fail(ex.Message);  }
        }

        [TestMethod]
        public void CreateMiddleNames_WithoutValue()
        {
            try
            {
                string actual = NameHelper.CreateMiddleNamesFromContact(_noValue);
                Assert.AreEqual(actual, string.Empty);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [TestMethod]
        public void CreateLastname_WithoutValue()
        {
            try
            {
                string actual = NameHelper.CreateLastnameFromContact(_noValue);
                Assert.AreEqual(actual, string.Empty);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }

        [TestMethod]
        public void CreateFirstname_FromFirstnameStringWithMiddleName()
        {
            try
            {
                string actual = NameHelper.CreateFirstnameFromContact("Liam John");
                string expected = "Liam";
                Assert.AreEqual(expected, actual);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }        
    }
}
