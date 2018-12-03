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
            clientData.CompletedDate_2009 = c2.UY12COM;
            clientData.CompletedDate_2010 = c2.UY13COM;
            clientData.CompletedDate_2011 = c2.UY14COM;
            clientData.CompletedDate_2012 = c2.UY15COM;
            clientData.CompletedDate_2013 = c2.UY16COM;
            clientData.CompletedDate_2014 = c2.UY17COM;
            clientData.CompletedDate_2015 = c2.UY18COM;
            clientData.CompletedDate_2016 = c2.UY19COM;
            clientData.CompletedDate_2017 = c2.UY20COM;
            clientData.CompletedDate_2018 = c2.UY21COM;
            clientData.CompletedDate_2019 = c2.UY22COM;
            clientData.Unsource = c2.UNSOURCE;
            return clientData;
        }
    }
}