using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;

namespace RIFTGroup.GCTSC.Business.Helpers
{
    public class ClientDataMappingHelper
    {
        public ClientData MapClientData(ClientData clientData, CONTACT1 c1, CONTACT2 c2)
        {
            clientData.Accountno = c1.ACCOUNTNO;
            clientData.Lastname = c1.LASTNAME;
            clientData.Phone1 = c1.PHONE1;
            clientData.Phone2 = c1.PHONE2;
            clientData.Phone3 = c1.PHONE3;
            clientData.Secr = c1.SECR;
            clientData.UconvDate = c2.UCONVDATE.ToString();
            clientData.UStage1Dat = c2.USTAGE1DAT.ToString();
            clientData.UEmailAddr = c2.UEMAILADDR;
            clientData.PostPreference = c2.UCPPOST;
            clientData.EmailPreference = c2.UCPEMAIL;
            clientData.PhonePreference = c2.UCPPHONE;
            clientData.SMSPreference = c2.UCPSMS;
            return clientData;
        }
    }
}
