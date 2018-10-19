using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using RIFTGroup.GCTSC.Business.Helpers;
using RIFTGroup.GCTSC.Core.Enums;
using RIFTGroup.GCTSC.Core.Model;
using RIFTGroup.GCTSC.Core.Helpers;

namespace RIFTGroup.GCTSC.Business
{
    public class GM_Repository
    {
        ClientDataMappingHelper _clientDataMapping;
        RefundHelper _refundHelper;
        public GM_Repository()
        {
            _clientDataMapping = new ClientDataMappingHelper();
            _refundHelper = new RefundHelper();
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

        public string GetKey3(string accountno)
        {
            string nino = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                nino = (from c in context.CONTACT1.Where(x => x.ACCOUNTNO == accountno) select c.KEY3).FirstOrDefault();
            }
            return nino;
        }

        public bool? GetChangeCommunicationPreference(string accountno, Enums.CommPreferenceType type)
        {
            bool? changedValue = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                switch (type)
                {
                    case Enums.CommPreferenceType.Email:
                        string emailValue = (from c in context.CONTACT2.Where(x => x.ACCOUNTNO == accountno) select c.UCPEMAIL).FirstOrDefault();
                        if (emailValue == "Yes")
                        {
                            changedValue = true;
                        }
                        else if (emailValue == "No")
                        {
                            changedValue = false;
                        }
                        break;
                    case Enums.CommPreferenceType.Phone:
                        string phoneValue = (from c in context.CONTACT2.Where(x => x.ACCOUNTNO == accountno) select c.UCPPHONE).FirstOrDefault();
                        if (phoneValue == "Yes")
                        {
                            changedValue = true;
                        }
                        else if (phoneValue == "No")
                        {
                            changedValue = false;
                        }
                        
                        break;
                    case Enums.CommPreferenceType.Post:
                        string postValue = (from c in context.CONTACT2.Where(x => x.ACCOUNTNO == accountno) select c.UCPPOST).FirstOrDefault();
                        if (postValue == "Yes")
                        {
                            changedValue = true;
                        }
                        else if (postValue == "No")
                        {
                            changedValue = false;
                        }
                        break;
                    case Enums.CommPreferenceType.SMS:
                        string smsValue = (from c in context.CONTACT2.Where(x => x.ACCOUNTNO == accountno) select c.UCPSMS).FirstOrDefault();
                        if (smsValue == "Yes")
                        {
                            changedValue = true;
                        }
                        else if (smsValue == "No")
                        {
                            changedValue = false;
                        }                        
                        break;
                }
            }
            return changedValue;
        }

        public Address GetAddress(string accountno)
        {
            Address address = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                CONTACT1 contact1Record = (from c in context.CONTACT1.Where(x => x.ACCOUNTNO == accountno) select c).FirstOrDefault();
                if (contact1Record != null)
                {
                    address = AddressHelper.ConvertToAddress(contact1Record);
                }
            }
            return address;
        }

        public string GetKey4(string accountno)
        {
            string utr = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                utr = (from c in context.CONTACT1.Where(x => x.ACCOUNTNO == accountno) select c.KEY4).FirstOrDefault();
            }
            return utr;
        }

        public string GetTranslatedCaseOwner(string accountno)
        {
            string translatedCaseOwner = string.Empty;
            string goldmineValue = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                goldmineValue = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.UBCASEOWN).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(goldmineValue))
            {
                using (RiftEntities context = new RiftEntities())
                {
                    string riftValue = (from r in context.WorkFlowUsernameLookups.Where(r => r.GoldMineUsername == goldmineValue) select r.WorkFlowUsername).FirstOrDefault();
                    if (!string.IsNullOrEmpty(riftValue))
                    {
                        translatedCaseOwner = riftValue.Split('@')[0];
                    }
                }
            }
            return translatedCaseOwner;
        }

        public string GetTranslatedUser(string user)
        {
            string translatedCaseOwner = string.Empty;
            if (!string.IsNullOrEmpty(user))
            {
                using (RiftEntities context = new RiftEntities())
                {
                    string riftValue = (from r in context.WorkFlowUsernameLookups.Where(r => r.GoldMineUsername == user) select r.WorkFlowUsername).FirstOrDefault();
                    if (!string.IsNullOrEmpty(riftValue))
                    {
                        translatedCaseOwner = riftValue.Split('@')[0];
                    }
                }
            }
            return translatedCaseOwner;
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

        public string GetDOB(string accountno)
        {
            string dob = string.Empty;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                DateTime? result = (from c in context.CONTACT2.Where(x => x.ACCOUNTNO == accountno) select c.UDOB).FirstOrDefault();
                if (result.HasValue)
                {
                    dob = result.Value.ToString("yyyy-MM-dd");
                }
            }
            return dob;
        }

        public ClientData GetClientData(string referenceNumber)
        {
            ClientData clientData = new ClientData();
            using (GoldmineEntities context = new GoldmineEntities())
            {
                CONTACT1 c1 = (from c in context.CONTACT1.Where(c => c.KEY5 == referenceNumber) select c).FirstOrDefault();
                if (c1 != null)
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

        public Refund GetRefundValues(string year, string accountno)
        {
            Refund refund = null;
            CONTACT2 contact2 = null;

            using (GoldmineEntities context = new GoldmineEntities())
            {
                contact2 = context.CONTACT2.Where(x => x.ACCOUNTNO == accountno).FirstOrDefault();
            }
            if (contact2 != null)
            {
                if (year == "14")
                {
                    refund = _refundHelper.CreateRefund(contact2.U14SERVT, contact2.UY14TYPE, contact2.UY14EXPREF, contact2.UY14EXPFEE, contact2.UY14FEEDAT, contact2.UY14SIGNED,
                        contact2.UY14ACTFEE, contact2.UY14ACTREF, contact2.UY14COM, contact2.UY14USER, 2011, accountno);
                }
                if (year == "15")
                {
                    refund = _refundHelper.CreateRefund(contact2.U15SERVT, contact2.UY15TYPE, contact2.UY15EXPREF, contact2.UY15EXPFEE, contact2.UY15FEEDAT, contact2.UY15SIGNED,
                        contact2.UY15ACTFEE, contact2.UY15ACTREF, contact2.UY15COM, contact2.UY15USER, 2012, accountno);
                }
                if (year == "16")
                {
                    refund = _refundHelper.CreateRefund(contact2.U16SERVT, contact2.U16TYPE, contact2.U16EXPREF, contact2.U16EXPFEE, contact2.U16FEEDAT, contact2.U16SIGNED,
                        contact2.UY16ACTFEE, contact2.UY16ACTREF, contact2.UY16COM, contact2.U16USER, 2013, accountno);
                }
                if (year == "17")
                {
                    refund = _refundHelper.CreateRefund(contact2.U17SERVT, contact2.U17TYPE, contact2.U17EXPREF, contact2.U17EXPFEE, contact2.U17FEEDAT, contact2.U17SIGNED,
                        contact2.UY17ACTFEE, contact2.UY17ACTREF, contact2.UY17COM, contact2.U17USER, 2014, accountno);
                }
                if (year == "18")
                {
                    refund = _refundHelper.CreateRefund(contact2.U18SERVT, contact2.U18TYPE, contact2.U18EXPREF, contact2.U18EXPFEE, contact2.U18FEEDAT, contact2.U18SIGNED,
                        contact2.UY18ACTFEE, contact2.UY18ACTREF, contact2.UY18COM, contact2.U18USER, 2015, accountno);
                }
                if (year == "19")
                {
                    refund = _refundHelper.CreateRefund(contact2.U19SERVT, contact2.U19TYPE, contact2.U19EXPREF, contact2.U19EXPFEE, contact2.U19FEEDAT, contact2.U19SIGNED,
                        contact2.UY19ACTFEE, contact2.UY19ACTREF, contact2.UY19COM, contact2.U19USER, 2016, accountno);
                }
                if (year == "20")
                {
                    refund = _refundHelper.CreateRefund(contact2.U20SERVT, contact2.U20TYPE, contact2.U20EXPREF, contact2.U20EXPFEE, contact2.U20FEEDAT, contact2.U20SIGNED,
                        contact2.UY20ACTFEE, contact2.UY20ACTREF, contact2.UY20COM, contact2.U20USER, 2017, accountno);
                }
                if (refund != null) { refund.TranslatedCaseowner = GetTranslatedUser(refund.User); }
            }

            return refund;
        }

        public string GetUstage1dat(string accountno)
        {
            DateTime? ustage1dat = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                ustage1dat = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.USTAGE1DAT).FirstOrDefault();
            }
            if (ustage1dat != null)
            {
                return ustage1dat.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        public string GetUconvdate(string accountno)
        {
            DateTime? uconvdate = null;
            using (GoldmineEntities context = new GoldmineEntities())
            {
                uconvdate = (from c in context.CONTACT2.Where(c => c.ACCOUNTNO == accountno) select c.UCONVDATE).FirstOrDefault();
            }
            if (uconvdate != null)
            {
                return uconvdate.Value.ToString();
            }
            else
            {
                return null;
            }
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
