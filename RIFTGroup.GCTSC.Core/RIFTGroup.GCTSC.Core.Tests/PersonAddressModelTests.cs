using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Core.ReponseObjects;

namespace RIFTGroup.GCTSC.Core.Tests
{
    [TestClass]
    public class PersonAddressModelTests
    {
        List<PersonAddress> _personAddresses;
        public PersonAddressModelTests()
        {
            _personAddresses = CreatePersonAddresses();
        }

        private List<PersonAddress> CreatePersonAddresses()
        {
            PersonAddress address1 = new PersonAddress()
            {
                address_id = "1",
                person_id = "1",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                id = "1",
                started_at = DateTime.Now.AddDays(-52),
                ended_at = DateTime.Now.AddDays(-22)
            };
            PersonAddress address2 = new PersonAddress()
            {
                address_id = "1",
                person_id = "1",
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                id = "2",
                started_at = DateTime.Now.AddDays(-21)                
            };
            List<PersonAddress> personAddresses = new List<PersonAddress>();
            personAddresses.Add(address1);
            personAddresses.Add(address2);
            return personAddresses;
        }

        [TestMethod]
        public void CheckForOtherActiveAddresses_ReturnsTrue_ForPersonAddressWithNoEndedDate()
        {
            bool otherActiveAddresses = _personAddresses.HasActiveAddresses();
            Assert.IsTrue(otherActiveAddresses);
        }
    }
}
