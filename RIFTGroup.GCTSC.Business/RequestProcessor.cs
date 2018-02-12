using System;
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
        public RequestProcessor()
        {
            _apiClient = new DataAPIClient();
            _gm_repo = new GM_Repository();
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
            return ro;
        }

        public ResultsObject ProcessContact2Requests(CONTACT2ChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            if (ro.Responses == null) { ro.Responses = new List<ResponseDetails>(); } ;
            ro = _gm_repo.GetReferenceNumberFromAccountno(ctResult.ACCOUNTNO, ro);
            ro.Accountno = ctResult.ACCOUNTNO;
            if (ctResult.uemailaddrChanged_bool)
            {
                string changedValue = _gm_repo.GetUemailaddr(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.UEMAILADDR, ro, changedValue);
            }
            if(ctResult.ustage1dat_bool)
            {
                string changedValue = _gm_repo.GetUstage1dat(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.USTAGE1DAT, ro, changedValue);
            }
            if(ctResult.uconvdate_bool)
            {
                string changedValue = _gm_repo.GetUconvdate(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.UCONVDATE, ro, changedValue);
            }
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
            if(ctResult.contactChanged_bool)
            {
                string changedValue = _gm_repo.GetContact(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.CONTACT, ro, changedValue);
            }
            if(ctResult.secrChanged_bool)
            {
                string changedValue = _gm_repo.GetSecr(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.SECR, ro, changedValue);
            }
            if(ctResult.LastnameChanged_bool)
            {
                string changedValue = _gm_repo.GetLastname(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.LASTNAME, ro, changedValue);
            }
            if(ctResult.phone1Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone1(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE1, ro, changedValue);
            }
            if(ctResult.phone2Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone2(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE2, ro, changedValue);
            }
            if(ctResult.phone3Changed_bool)
            {
                string changedValue = _gm_repo.GetPhone3(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE3, ro, changedValue);
            }
            return ro;

        }
    }
}
