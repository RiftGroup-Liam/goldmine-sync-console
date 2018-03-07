using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public class NameHelper
    {
        public static string CreateFirstnameFromContact(string contact)
        {
            string firstname = string.Empty;
            if (string.IsNullOrEmpty(contact)) { return firstname; }
            else
            {
                return contact.Split(' ').First();
            }
        }

        public static string CreateMiddleNamesFromContact(string contact)
        {
            string middleNames = string.Empty;
            if (string.IsNullOrEmpty(contact)) { return middleNames; }
            else
            {
                string firstname = CreateFirstnameFromContact(contact);
                string lastname = CreateLastnameFromContact(contact);
                middleNames = contact.Replace(firstname, string.Empty);
                middleNames = middleNames.Replace(lastname, string.Empty);
                middleNames = middleNames.Trim();
                return middleNames;
            }            
        }

        public static string CreateLastnameFromContact(string contact)
        {
            string lastname = string.Empty;
            if (string.IsNullOrEmpty(contact)) { return lastname; }
            else
            {
                return contact.Split(' ').Last();
            }
        }
    }
}
