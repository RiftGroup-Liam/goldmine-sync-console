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
            OK,
            UNAUTHORISED,
            NOT_FOUND,
            FAILED
        }

        public enum SendRequest
        {
            KEY5,
            CONTACT,
            SECR,
            LASTNAME,
            PHONE1,
            PHONE2,
            PHONE3,
            UCONVDATE,
            USTAGE1DAT,
            UEMAILADDR,
            CONTSUPREF
        }

        public enum PhoneNumberKind
        {
            Work = 1,
            Home = 2,
            Mobile = 3
        }
    }
}
