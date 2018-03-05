using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public class PhoneNumberHelper
    {
        public static string CreateSubscriberNumber(string phoneNumber)
        {
            string subscriberNumber = string.Empty;
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                subscriberNumber = Regex.Replace(phoneNumber, "[^0-9.]", "");
                if (subscriberNumber.Substring(0, 2) == "44")
                {
                    subscriberNumber = subscriberNumber.Substring(2, subscriberNumber.Length - 2);
                }
                else if (subscriberNumber.Substring(0, 1) == "0")
                {
                    subscriberNumber = subscriberNumber.Substring(1, subscriberNumber.Length - 1);
                }
            }
            return subscriberNumber;
        }
    }
}
