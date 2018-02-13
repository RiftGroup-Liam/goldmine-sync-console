using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Enums
{
    public class Enums
    {
        public enum SendResponse
        {
            OK = 1,
            UNAUTHORISED = 2,
            NOT_FOUND = 3,
            FAILED = 4
        }

        public enum SendRequest
        {
            KEY5 = 1,
            CONTACT = 2,
            SECR = 3,
            LASTNAME = 4,
            PHONE1 = 5,
            PHONE2 = 6,
            PHONE3 = 7,
            UCONVDATE = 8,
            USTAGE1DAT = 9,
            UEMAILADDR = 10,
            CONTSUPREF = 11,
            CREATE = 12
        }

        public enum PhoneNumberKind
        {
            Work = 1,
            Home = 2,
            Mobile = 3,
            General = 4
        }
    }
}
