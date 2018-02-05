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
            if(ctResult.contsuprefChanged_bool)
            {
                ro = _gm_repo.GetReferenceNumberFromRecid(ctResult.recid, ro);
                ro = _gm_repo.GetAccountnoFromRecid(ctResult.recid, ro);
                ro.ChangedValue = _gm_repo.GetContsupref(ctResult.recid);
                ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.CONTSUPREF, ro);
            }
            return ro;
        }

        public ResultsObject ProcessContact2Requests(CONTACT2ChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            ro = _gm_repo.GetReferenceNumberFromAccountno(ctResult.ACCOUNTNO, ro);
            ro.Accountno = ctResult.ACCOUNTNO;
            if (ctResult.uemailaddrChanged_bool)
            {
                ro.ChangedValue = _gm_repo.GetUclientsta(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdateEmailAddressRequest(Enums.SendRequest.UEMAILADDR, ro);
            }
            if(ctResult.uclientstaChanged_bool)
            {
                ro.ChangedValue = _gm_repo.GetUclientsta(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.UCLIENTSTA, ro);
            }
            return ro;
        }

        public ResultsObject ProcessContact1Requests(CONTACT1ChangeTracking_Result ctResult)
        {
            ResultsObject ro = new ResultsObject();
            ro = _gm_repo.GetReferenceNumberFromAccountno(ctResult.ACCOUNTNO, ro);
            ro.Accountno = ctResult.ACCOUNTNO;
            if (ctResult.key5Changed_bool)
            {
                ro.ChangedValue = _gm_repo.GetKey5(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.KEY5, ro);
            }
            if(ctResult.contactChanged_bool)
            {
                ro.ChangedValue = _gm_repo.GetContact(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.CONTACT, ro);
            }
            if(ctResult.secrChanged_bool)
            {
                ro.ChangedValue = _gm_repo.GetSecr(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.SECR, ro);
            }
            if(ctResult.LastnameChanged_bool)
            {
                ro.ChangedValue = _gm_repo.GetLastname(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.LASTNAME, ro);
            }
            if(ctResult.phone1Changed_bool)
            {
                ro.ChangedValue = _gm_repo.GetPhone1(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE1, ro);
            }
            if(ctResult.phone2Changed_bool)
            {
                ro.ChangedValue = _gm_repo.GetPhone2(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE2, ro);
            }
            if(ctResult.phone3Changed_bool)
            {
                ro.ChangedValue = _gm_repo.GetPhone3(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePhoneNumberRequest(Enums.SendRequest.PHONE3, ro);
            }
            if(ctResult.key2Changed_bool)
            {
                ro.ChangedValue = _gm_repo.GetKey2(ctResult.ACCOUNTNO);
                ro = _apiClient.SendUpdatePersonRequest(Enums.SendRequest.KEY2, ro);
            }
            return ro;

        }
    }
}
