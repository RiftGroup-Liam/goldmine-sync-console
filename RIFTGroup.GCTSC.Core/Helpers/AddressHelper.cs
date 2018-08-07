using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core.Model;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public static class AddressHelper
    {
        public static Address ConvertToAddress(CONTACT1 contact1Record)
        {
            return new Address()
            {
                first_line = contact1Record.ADDRESS1,
                second_line = contact1Record.ADDRESS2,
                postcode = contact1Record.ZIP,
                county = contact1Record.STATE,
                country = contact1Record.COUNTRY,
                town = contact1Record.CITY
            };
        }

        public static bool CheckForFullAddressUpdate(CONTACT1ChangeTracking_Result ctResult)
        {
            bool fullUpdate = false;
            if (ctResult.address1Changed_bool || ctResult.address2Changed_bool)
            { fullUpdate = true; }
            return fullUpdate;
        }
    }
}
