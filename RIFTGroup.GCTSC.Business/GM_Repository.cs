using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using RIFTGroup.GCTSC.Business.Helpers;

namespace RIFTGroup.GCTSC.Business
{
    public class GM_Repository
    {
        ClientDataMappingHelper _clientDataMapping;
        public GM_Repository()
        {
            _clientDataMapping = new ClientDataMappingHelper();
        }
        public ResultsObject GetReferenceNumberFromRecid(string recid, ResultsObject ro)
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                string accountno = (from c in context.CONTSUPPs.Where(c => c.recid == recid) select c.ACCOUNTNO).FirstOrDefault();
                if (!string.IsNullOrEmpty(accountno))
                {
                    ro.ReferenceNumber = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.KEY5).FirstOrDefault();
                }
            }
            return ro;
        }

        public ResultsObject GetAccountnoFromRecid(string recid, ResultsObject ro)
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                ro.Accountno = (from c in context.CONTSUPPs.Where(c => c.recid == recid) select c.ACCOUNTNO).FirstOrDefault();
            }
            return ro;
        }

        public ResultsObject GetReferenceNumberFromAccountno(string accountno, ResultsObject ro)
        {
            using (GoldmineEntities context = new GoldmineEntities())
            {
                ro.ReferenceNumber = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.KEY5).FirstOrDefault();
            }
            return ro;
        }

        public string GetContsupref(string recid)
        {
            string contsupref = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                contsupref = (from c in context.CONTSUPPs.Where(c => c.recid == recid) select c.CONTSUPREF).FirstOrDefault();
            }
            return contsupref;
        }

        public string GetKey5(string accountno)
        {
            string key5 = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                key5 = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.KEY5).FirstOrDefault();
            }
            return key5;
        }

        public string GetSecr(string accountno)
        {
            string secr = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                secr = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.SECR).FirstOrDefault();
            }
            return secr;
        }

        public string GetContact(string accountno)
        {
            string contact = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                contact = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.CONTACT).FirstOrDefault();
            }
            return contact;
        }

        public ClientData GetClientData(string referenceNumber)
        {
            ClientData clientData = new ClientData();
            using (GoldmineEntities context = new GoldmineEntities())
            {
                CONTACT1 c1 = (from c in context.CONTACT1.Where(c => c.KEY5 == referenceNumber) select c).FirstOrDefault();
                if(c1!=null)
                {
                    CONTACT2 c2 = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == c1.ACCOUNTNO) select c).FirstOrDefault();
                    clientData = _clientDataMapping.MapClientData(clientData, c1, c2);
                    clientData.Key5 = referenceNumber;
                }
            }
            return clientData;
        }

        public string GetLastname(string accountno)
        {
            string lastname = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                lastname = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.LASTNAME).FirstOrDefault();
            }
            return lastname;
        }

        public string GetPhone1(string accountno)
        {
            string phone1 = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                phone1 = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.PHONE1).FirstOrDefault();
            }
            return phone1;
        }

        public string GetPhone2(string accountno)
        {
            string phone2 = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                phone2 = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.PHONE2).FirstOrDefault();
            }
            return phone2;
        }

        public string GetPhone3(string accountno)
        {
            string phone3 = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                phone3 = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.PHONE3).FirstOrDefault();
            }
            return phone3;
        }

        public string GetUstage1dat(string accountno)
        {
            DateTime? ustage1dat = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                ustage1dat = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.USTAGE1DAT).FirstOrDefault();
            }
            return ustage1dat.Value.ToString();
        }

        public string GetUconvdate(string accountno)
        {
            DateTime? uconvdate = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                uconvdate = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.UCONVDATE).FirstOrDefault();
            }
            return uconvdate.Value.ToString();
        }

        public string GetUemailaddr(string accountno)
        {
            string uemailAddr = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                uemailAddr = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.UEMAILADDR).FirstOrDefault();
            }
            return uemailAddr;
        }
    }
}
