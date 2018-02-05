using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;

namespace RIFTGroup.GCTSC.Business
{
    public class GM_Repository
    {
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

        public string GetUclientsta(string accountno)
        {
            string uclientsta = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                uclientsta = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.UCLIENTSTA).FirstOrDefault();
            }
            return uclientsta;
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

        public string GetKey2(string accountno)
        {
            string key2 = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                key2 = (from c in context.CONTACT1.Where(c => c.ACCOUNTNO == accountno) select c.KEY2).FirstOrDefault();
            }
            return key2;
        }
    }
}
