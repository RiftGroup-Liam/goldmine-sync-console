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
            NOT_FOUND
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
            KEY2,
            UCLIENTSTA,
            UEMAILADDR,
            CONTSUPREF
        }
    }
}
