using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core.Enums;
using RestSharp;
using RIFTGroup.GCTSC.Core.ReponseObjects;
using RIFTGroup.GCTSC.Core.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace RIFTGroup.GCTSC.Core
{
    public class DataAPIClient
    {
        string _baseAddress;
        string _apiToken;
        RestClient _restClient;
        AppSettings _appSettings;
        public DataAPIClient()
        {
            _appSettings = new AppSettings();
            _baseAddress = _appSettings.ApiURL;
            _apiToken = _appSettings.APIToken;
            _restClient = new RestClient(_baseAddress);
        }

        public ResultsObject SendUpdatePersonRequest(Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            ro.Responses = new List<ResponseDetails>();
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/people/" + personId, Method.PATCH);
                request.AddHeader("Authentication-Token", _apiToken);
                request = RequestBodyHelper.CreateUpdatePersonRequestBody(requestType, ro.ChangedValue, request);
                IRestResponse response = _restClient.Execute(request);
                UpdatePeopleResponse updateResponse = JsonConvert.DeserializeObject<UpdatePeopleResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails() {
                                                        URL = response.ResponseUri.ToString(),
                                                        SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode) });
            }
            return ro;
        }
        public ResultsObject SendUpdatePhoneNumberRequest(Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            return ro;
        }

        public ResultsObject SendUpdateEmailAddressRequest(Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            UpdateCurrentEmailsToNonActive(ro);
            ro = CreateNewEmailAddress(ro);
            return ro;
        }

        private void UpdateCurrentEmailsToNonActive(ResultsObject ro)
        {
            List<EmailResponse> emailAddresses = GetEmailAddresses(ro);
            if(emailAddresses.Count>0)
            {
                foreach(EmailResponse address in emailAddresses)
                {
                    IRestRequest request = new RestRequest("/person/email_addresses/" + address.Id, Method.PATCH);
                    request.AddHeader("Authentication-Token", _apiToken);
                    request = RequestBodyHelper.CreateUpdateEmailAddressToNonActiveBody(ro.ChangedValue, request);
                    IRestResponse response = _restClient.Execute(request);
                    UpdateEmailResponse updateResponse = JsonConvert.DeserializeObject<UpdateEmailResponse>(response.Content);
                    ro.Responses.Add(new ResponseDetails()
                    {
                        URL = response.ResponseUri.ToString(),
                        SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode)
                    });
                }
            }
        }

        private List<EmailResponse> GetEmailAddresses(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<EmailResponse> emailResponses = new List<EmailResponse>();
            ro.Responses = new List<ResponseDetails>();
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/person/email_addresses?person_id=" + personId);
                request.AddHeader("Authentication-Token", _apiToken);
                IRestResponse response = _restClient.Execute(request);
                emailResponses = JsonConvert.DeserializeObject<List<EmailResponse>>(response.Content);
                ro.Responses.Add(new ResponseDetails() { URL = response.ResponseUri.ToString(),
                                                            SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode) });
            }
            return emailResponses;
        }

        private ResultsObject CreateNewEmailAddress(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);            
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/person/email_addresses/", Method.POST);
                request.AddHeader("Authentication-Token", _apiToken);
                request = RequestBodyHelper.CreateEmailAddressBody(ro.ChangedValue, personId, request);
                IRestResponse response = _restClient.Execute(request);
                CreateEmailResponse createEmailResponse = JsonConvert.DeserializeObject<CreateEmailResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails()
                {
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode)
                });
            }
            return ro;
        }

        private string GetPersonId(string referenceNumber)
        {
            string personId = string.Empty;
            IRestRequest request = new RestRequest("/people?goldmine_customer_number=" + referenceNumber);
            request.AddHeader("Authentication-Token", _apiToken);
            IRestResponse response = _restClient.Execute(request);
            List<PeopleResponse> peopleResponse = JsonConvert.DeserializeObject<List<PeopleResponse>>(response.Content);
            personId = peopleResponse[0].Id;
            return personId;
        }
    }
}
