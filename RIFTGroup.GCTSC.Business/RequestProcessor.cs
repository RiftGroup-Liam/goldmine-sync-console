﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.Enums;

namespace RIFTGroup.GCTSC.Business
{
    public class RequestProcessor
    {
        DataAPIClient _apiClient;
        GM_Repository _gm_repo;
        ApplicationLogging _applicationLogging;
        public RequestProcessor()
        {
            _apiClient = new DataAPIClient();
            _gm_repo = new GM_Repository();
            _applicationLogging = new ApplicationLogging();
        }
        public ResultsObject ProcessContsuppRequests(CONTSUPPChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            if (ro.Responses == null) { ro.Responses = new List<ResponseDetails>(); };
            if (ctResult.contsuprefChanged_bool)
            {
                ro = _gm_repo.GetReferenceNumberFromRecid(ctResult.recid, ro);
                ro = _gm_repo.GetAccountnoFromRecid(ctResult.recid, ro);
                string changedValue = _gm_repo.GetContsupref(ctResult.recid);
                ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.CONTSUPREF, ro, changedValue);
            }
            _applicationLogging.Log(ro);
            return ro;
        }

        public ResultsObject ProcessContact2Requests(CONTACT2ChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            if (ro.Responses == null) { ro.Responses = new List<ResponseDetails>(); };
            ro = _gm_repo.GetReferenceNumberFromAccountno(ctResult.ACCOUNTNO, ro);
            ro.Accountno = ctResult.ACCOUNTNO;
            if (ctResult.uemailaddrChanged_bool)
            {
                string changedValue = _gm_repo.GetUemailaddr(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.UEMAILADDR, ro, changedValue);
            }
            if (ctResult.ustage1dat_bool)
            {
                string changedValue = _gm_repo.GetUstage1dat(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.USTAGE1DAT, ro, changedValue);
            }
            if (ctResult.uconvdate_bool)
            {
                string changedValue = _gm_repo.GetUconvdate(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.UCONVDATE, ro, changedValue);
            }
            if (ctResult.ubcaseown_bool)
            {
                string changedValue = _gm_repo.GetTranslatedCaseOwner(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateCaseOwnerRequest(Enums.SendRequest.UBCASEOWN, ro, changedValue);
            }
            if (ctResult.ucpphone_bool)
            {
                if (_gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Phone).HasValue)
                {
                    bool changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Phone).Value;
                    ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, changedValue);
                }
            }
            if (ctResult.ucppost_bool)
            {
                if (_gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Post).HasValue)
                {
                    bool changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Post).Value;
                    ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, changedValue);
                }
            }
            if (ctResult.ucpemail_bool)
            {
                if (_gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Email).HasValue)
                {
                    bool changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Email).Value;
                    ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Email, ro, changedValue);
                }
            }
            if (ctResult.ucpsms_bool)
            {
                if (_gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.SMS).HasValue)
                {
                    bool changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.SMS).Value;
                    ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.SMS, ro, changedValue);
                }
            }
            ro = DoCompletedUpdates(ctResult, ro);
            _applicationLogging.Log(ro);
            return ro;
        }

        public ResultsObject ProcessContact1Requests(CONTACT1ChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            ro.Responses = new List<ResponseDetails>();
            ro = _gm_repo.GetReferenceNumberFromAccountno(ctResult.ACCOUNTNO, ro);
            ro.Accountno = ctResult.ACCOUNTNO;
            if (ctResult.key5Changed_bool)
            {
                string changedValue = _gm_repo.GetKey5(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.KEY5, ro, changedValue);
            }
            if (ctResult.contactChanged_bool)
            {
                string changedValue = _gm_repo.GetContact(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.CONTACT, ro, changedValue);
            }
            if (ctResult.secrChanged_bool)
            {
                string changedValue = _gm_repo.GetSecr(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.SECR, ro, changedValue);
            }
            if (ctResult.LastnameChanged_bool)
            {
                string changedValue = _gm_repo.GetLastname(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.LASTNAME, ro, changedValue);
            }
            if (ctResult.phone1Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone1(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE1, ro, changedValue);
            }
            if (ctResult.phone2Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone2(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE2, ro, changedValue);
            }
            if (ctResult.phone3Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone3(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE3, ro, changedValue);
            }
            _applicationLogging.Log(ro);
            return ro;

        }

        public ResultsObject ProcessCreatePersonRequest(ClientData clientData)
        {
            ResultsObject ro = new ResultsObject();
            if (ro.Responses == null) { ro.Responses = new List<ResponseDetails>(); };
            ro.Accountno = clientData.Accountno;
            ro.ReferenceNumber = clientData.Key5;
            ro = _apiClient.CreatePersonRequest(ro, clientData);
            ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.UEMAILADDR, ro, clientData.UEmailAddr);
            ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE1, ro, clientData.Phone1);
            ro = CreateCommunicationPreferences(ro, clientData);
            _applicationLogging.Log(ro);
            return ro;
        }

        private ResultsObject CreateCommunicationPreferences(ResultsObject ro, ClientData clientData)
        {
            if(clientData.PhonePreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, true);
            }
            else if(clientData.PhonePreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, false);
            }

            if(clientData.PostPreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, true);
            }
            else if(clientData.PostPreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, false);
            }

            if(clientData.SMSPreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, true);
            }
            else if(clientData.SMSPreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, false);
            }

            if (clientData.EmailPreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Email, ro, true);
            }
            else if (clientData.EmailPreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Email, ro, false);
            }
            return ro;
        }

        private ResultsObject DoCompletedUpdates(CONTACT2ChangeTracking_Result ctResult, ResultsObject ro)
        {
            if (ctResult.uy12com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY12, ro);
            }
            if (ctResult.uy13com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY13, ro);
            }
            if (ctResult.uy14com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY14, ro);
            }
            if (ctResult.uy15com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY15, ro);
            }
            if (ctResult.uy16com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY16, ro);
            }
            if (ctResult.uy17com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY17, ro);
            }
            if (ctResult.uy18com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY18, ro);
            }
            if (ctResult.uy19com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY19, ro);
            }
            if (ctResult.uy20com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY20, ro);
            }
            if (ctResult.uy21com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY21, ro);
            }
            if (ctResult.uy22com_bool)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY22, ro);
            }
            return ro;
        }

        public string ProcessPersonIdFetch(string referenceNumber)
        {
            string personId = string.Empty;
            personId = _apiClient.GetPersonId(referenceNumber);
            return personId;
        }
    }
}
