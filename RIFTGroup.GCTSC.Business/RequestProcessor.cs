using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.Enums;
using RIFTGroup.GCTSC.Core.Model;
using RIFTGroup.GCTSC.Core.Helpers;

namespace RIFTGroup.GCTSC.Business
{
    public class RequestProcessor
    {
        private DataAPIClient _apiClient;
        private ApplicationLogging _applicationLogging;
        private GM_Repository _gm_repo;

        public RequestProcessor()
        {
            _apiClient = new DataAPIClient();
            _gm_repo = new GM_Repository();
            _applicationLogging = new ApplicationLogging();
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
            if (ctResult.key3Changed_bool)
            {
                //NINO
                string changedValue = _gm_repo.GetKey3(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateNINORequest(ro, changedValue);
            }
            if (ctResult.key4Changed_bool)
            {
                //UTR
                string changedValue = _gm_repo.GetKey4(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateUTRRequest(ro, changedValue);
            }
            if (ctResult.address1Changed_bool || ctResult.address2Changed_bool || ctResult.cityChanged_bool
                || ctResult.countryChanged_bool || ctResult.stateChanged_bool || ctResult.zipChanged_bool)
            {
                Address address = _gm_repo.GetAddress(ctResult.ACCOUNTNO);
                DoAddressUpdate(address, ro, ctResult);
            }



            _applicationLogging.Log(ro);
            return ro;
        }

        public ResultsObject DoAddressUpdate(Address address, ResultsObject ro, CONTACT1ChangeTracking_Result ctResult)
        {
            string dataapi_addressId = _apiClient.GetExistingAddress(address.first_line, address.postcode);
            if (!string.IsNullOrEmpty(dataapi_addressId))
            {
                bool fullAddressUpdate = AddressHelper.CheckForFullAddressUpdate(ctResult);
                if (!fullAddressUpdate)
                {
                    bool othersLinkedToThisAddress = _apiClient.CheckForOthersLinkedToThisAddress(dataapi_addressId);
                    if (othersLinkedToThisAddress)
                    {
                        string personId = _apiClient.GetPersonId(ro.ReferenceNumber);
                        bool imLinkedToAddress = _apiClient.CheckIfImLinkedToAddress(dataapi_addressId, personId);
                        if (!imLinkedToAddress)
                        {
                            CreateAddress(address, ro);
                        }
                    }
                    else
                    {
                        _apiClient.AmendCurrentAddress(address, ro, dataapi_addressId);
                    }
                }
                else
                {
                    string personId = _apiClient.GetPersonId(ro.ReferenceNumber);
                    bool imLinkedToAddress = _apiClient.CheckIfImLinkedToAddress(dataapi_addressId, personId);
                    if (!imLinkedToAddress)
                    {
                        CreateAddress(address, ro);
                    }
                }
            }
            else
            {
                CreateAddress(address, ro);
            }
            FINISH:
            return ro;
        }

        public string CreateAddress(Address address, ResultsObject ro)
        {
            string addressId = string.Empty;
            string personId = _apiClient.GetPersonId(ro.ReferenceNumber);
            if (!string.IsNullOrEmpty(personId))
            {
                bool otherActiveAddresses = _apiClient.CheckForOtherActiveAddresses(personId);

                if (otherActiveAddresses)
                {
                    _apiClient.SetOtherAddressesToInactive(ro);
                }

                addressId = _apiClient.CreateAddress(address, ro);
                if (!string.IsNullOrEmpty(addressId))
                {
                    _apiClient.CreatePersonAddress(addressId, personId, ro);
                }
            }
            return addressId;
        }

        public ResultsObject ProcessContact2Requests(CONTACT2ChangeTracking_Result ctResult, ClientData clientData)
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
                if (!string.IsNullOrEmpty(clientData.UStage1Dat))
                {
                    ro = _apiClient.SendUpdateAllClaimsStatus(ro, 3);
                }
            }
            if (ctResult.uconvdate_bool)
            {
                string changedValue = _gm_repo.GetUconvdate(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.UCONVDATE, ro, changedValue);
                if (!string.IsNullOrEmpty(clientData.UconvDate))
                {
                    ro = _apiClient.SendUpdateAllClaimsStatus(ro, 4);
                }
            }
            if (ctResult.ubcaseown_bool)
            {
                string changedValue = _gm_repo.GetTranslatedCaseOwner(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateCaseOwnerRequest(Enums.SendRequest.UBCASEOWN, ro, changedValue);
            }
            if (ctResult.ucpphone_bool)
            {
                bool? changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Phone);
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, changedValue);
            }
            if (ctResult.ucppost_bool)
            {
                bool? changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Post);
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, changedValue);
            }
            if (ctResult.ucpemail_bool)
            {
                bool? changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.Email);
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Email, ro, changedValue);
            }
            if (ctResult.ucpsms_bool)
            {
                bool? changedValue = _gm_repo.GetChangeCommunicationPreference(ctResult.ACCOUNTNO, Enums.CommPreferenceType.SMS);
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.SMS, ro, changedValue);
            }
            if (ctResult.udob_bool)
            {
                string changedValue = _gm_repo.GetDOB(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateDOBRequest(ro, changedValue);
            }
            if (ctResult.u14servt_bool || ctResult.uy14type_bool || ctResult.uy14expref_bool || ctResult.uy14expfee_bool || ctResult.uy14user_bool
                || ctResult.uy14feedat_bool || ctResult.uy14signed_bool || ctResult.uy14actfee_bool || ctResult.uy14actref_bool || ctResult.uy14com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("14", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u15servt_bool || ctResult.uy15type_bool || ctResult.uy15expref_bool || ctResult.uy15expfee_bool || ctResult.uy15user_bool
                || ctResult.uy15feedat_bool || ctResult.uy15signed_bool || ctResult.uy15actfee_bool || ctResult.uy15actref_bool || ctResult.uy15com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("15", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u16servt_bool || ctResult.u16type_bool || ctResult.u16expref_bool || ctResult.u16expfee_bool || ctResult.u16user_bool
                || ctResult.u16feedat_bool || ctResult.u16signed_bool || ctResult.u16actfee_bool || ctResult.u16actref_bool || ctResult.uy16com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("16", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u17servt_bool || ctResult.u17type_bool || ctResult.u17expref_bool || ctResult.u17expfee_bool || ctResult.u17user_bool
                || ctResult.u17feedat_bool || ctResult.u17signed_bool || ctResult.u17actfee_bool || ctResult.u17actref_bool || ctResult.uy17com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("17", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u18servt_bool || ctResult.u18type_bool || ctResult.u18expref_bool || ctResult.u18expfee_bool || ctResult.u18user_bool
                || ctResult.u18feedat_bool || ctResult.u18signed_bool || ctResult.u18actfee_bool || ctResult.u18actref_bool || ctResult.uy18com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("18", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u19servt_bool || ctResult.u19type_bool || ctResult.u19expref_bool || ctResult.u19expfee_bool || ctResult.u19user_bool
                || ctResult.u19feedat_bool || ctResult.u19signed_bool || ctResult.u19actfee_bool || ctResult.u19actref_bool || ctResult.uy19com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("19", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            if (ctResult.u20servt_bool || ctResult.u20type_bool || ctResult.u20expref_bool || ctResult.u20expfee_bool || ctResult.u20user_bool
                || ctResult.u20feedat_bool || ctResult.u20signed_bool || ctResult.u20actfee_bool || ctResult.u20actref_bool || ctResult.uy20com_bool)
            {
                Refund refund = _gm_repo.GetRefundValues("20", ctResult.ACCOUNTNO);
                ro = _apiClient.SendRefundUpdateRequest(ro, refund);
            }
            ro = DoCompletedUpdates(ctResult, ro);
            _applicationLogging.Log(ro);
            return ro;
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

        public string ProcessPersonIdFetch(string referenceNumber)
        {
            string personId = string.Empty;
            personId = _apiClient.GetPersonId(referenceNumber);
            return personId;
        }

        private ResultsObject CreateCommunicationPreferences(ResultsObject ro, ClientData clientData)
        {
            if (clientData.PhonePreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, true);
            }
            else if (clientData.PhonePreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Phone, ro, false);
            }

            if (clientData.PostPreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, true);
            }
            else if (clientData.PostPreference == "No")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, false);
            }

            if (clientData.SMSPreference == "Yes")
            {
                ro = _apiClient.SendUpdateCommunicationPreference(Enums.CommPreferenceType.Post, ro, true);
            }
            else if (clientData.SMSPreference == "No")
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
            ClientData clientData = _gm_repo.GetClientData(_gm_repo.GetKey5(ctResult.ACCOUNTNO));

            if (ctResult.uy12com_bool && clientData.CompletedDate_2009 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY12, ro, 8);
            }
            if (ctResult.uy13com_bool && clientData.CompletedDate_2010 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY13, ro, 8);
            }
            if (ctResult.uy14com_bool && clientData.CompletedDate_2011 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY14, ro, 8);
            }
            if (ctResult.uy15com_bool && clientData.CompletedDate_2012 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY15, ro, 8);
            }
            if (ctResult.uy16com_bool && clientData.CompletedDate_2013 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY16, ro, 8);
            }
            if (ctResult.uy17com_bool && clientData.CompletedDate_2014 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY17, ro, 8);
            }
            if (ctResult.uy18com_bool && clientData.CompletedDate_2015 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY18, ro, 8);
            }
            if (ctResult.uy19com_bool && clientData.CompletedDate_2016 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY19, ro, 8);
            }
            if (ctResult.uy20com_bool && clientData.CompletedDate_2017 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY20, ro, 8);
            }
            if (ctResult.uy21com_bool && clientData.CompletedDate_2018 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY21, ro, 8);
            }
            if (ctResult.uy22com_bool && clientData.CompletedDate_2019 != null)
            {
                ro = _apiClient.SendUpdateClaimStatus(Enums.Year.UY22, ro, 8);
            }
            return ro;
        }
    }
}