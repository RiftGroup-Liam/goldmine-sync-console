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
            CREATE = 12,
            UBCASEOWN = 13,
            EMAILPREFERENCE = 14,
            PHONEPREFERENCE = 15,
            POSTPREFERENCE = 16,
            SMSPREFERENCE = 17,
            UTR = 18,
            NINO = 19,
            DOB = 20,
            ADDRESS = 21,
            CREATEPERSONADDRESS = 22,
            CREATEADDRESS = 23
        }

        public enum PhoneNumberKind
        {
            Work = 1,
            Home = 2,
            Mobile = 3,
            General = 4
        }

        public enum CommPreferenceType
        {
            Phone,
            Email,
            SMS,
            Post
        }

        public enum Year
        {
            UY12 = 2010,
            UY13 = 2011,
            UY14 = 2012,
            UY15 = 2013,
            UY16 = 2014,
            UY17 = 2015,
            UY18 = 2016,
            UY19 = 2017,
            UY20 = 2018,
            UY21 = 2019,
            UY22 = 2020
        }
    }
}
