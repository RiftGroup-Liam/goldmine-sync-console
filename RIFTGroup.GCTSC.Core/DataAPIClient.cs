﻿using System;
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

        public ResultsObject SendUpdatePersonRequest(Enums.Enums.SendRequest requestType, ResultsObject ro, string changedValue)
        {
            string personId = GetPersonId(ro.ReferenceNumber);            
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/people/" + personId, Method.PATCH);
                request.AddHeader("Authentication-Token", _apiToken);
                request = RequestBodyHelper.CreateUpdatePersonRequestBody(requestType, changedValue, request);

                if (_appSettings.RunAsConsole) { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                UpdatePeopleResponse updateResponse = JsonConvert.DeserializeObject<UpdatePeopleResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails() {
                                                        SendRequest = requestType,
                                                        ChangedValue = changedValue,
                                                        URL = response.ResponseUri.ToString(),
                                                        SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                                                        ResponseContent = response.Content});
            }
            return ro;
        }
        
        public ResultsObject SendUpdatePhoneNumberRequest(Enums.Enums.SendRequest requestType, ResultsObject ro, string changedValue)
        {
            ro = CreateNewPhoneNumber(ro, requestType, changedValue);
            if (ro.Responses.Last().SendResponse == Enums.Enums.SendResponse.OK)
            {
                UpdateCurrentPhoneNumbersToNonActive(ro, changedValue);
            }
            return ro;
        }        

        public ResultsObject SendUpdateEmailAddressRequest(Enums.Enums.SendRequest requestType, ResultsObject ro, string changedValue)
        {
            ro = CreateNewEmailAddress(ro, requestType, changedValue);
            if (ro.Responses.Last().SendResponse == Enums.Enums.SendResponse.OK)
            {
                UpdateCurrentEmailsToNonActive(ro, changedValue);
            }
            return ro;
        }

        public string GetPersonId(string referenceNumber)
        {
            string personId = string.Empty;
            IRestRequest request = new RestRequest("/people?goldmine_customer_number=" + referenceNumber);
            request.AddHeader("Authentication-Token", _apiToken);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            if (response.Content != "[]" && response.Content != "")
            {
                List<PeopleResponse> peopleResponse = JsonConvert.DeserializeObject<List<PeopleResponse>>(response.Content);
                if (peopleResponse.Count != 0)
                {
                    personId = peopleResponse[0].Id;
                }
            }
            
            return personId;
        }
        
        public ResultsObject CreatePersonRequest(ResultsObject ro, ClientData clientData)
        {
            IRestRequest request = new RestRequest("/people/", Method.POST);
            request.AddHeader("Authentication-Token", _apiToken);

            request = RequestBodyHelper.CreatePersonRequestBody(request, clientData);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            CreatePersonResponse creatgePersonResponse= JsonConvert.DeserializeObject<CreatePersonResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                SendRequest = Enums.Enums.SendRequest.CREATE,
                ChangedValue = "CREATE",
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            return ro;
        }

        private void UpdateCurrentPhoneNumbersToNonActive(ResultsObject ro, string changedValue)
        {
            List<PhoneNumberResponse> phoneNumbers = GetPhoneNumbers(ro);
            if(phoneNumbers.Count > 0)
            {
                foreach(PhoneNumberResponse phoneNumber in phoneNumbers)
                {
                    if (phoneNumber.Subscriber_number != PhoneNumberHelper.CreateSubscriberNumber(changedValue))
                    {
                        IRestRequest request = new RestRequest("person/phone_numbers/" + phoneNumber.Id, Method.PATCH);
                        request.AddHeader("Authentication-Token", _apiToken);
                        request = RequestBodyHelper.CreateUpdatePhoneNumberToNonActiveRequestBody(request);

                        if (_appSettings.RunAsConsole)
                        { Console.WriteLine("Sending: {0}\n", request.Resource); }
                        IRestResponse response = _restClient.Execute(request);
                        if (_appSettings.RunAsConsole)
                        {
                            Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                            Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                        }

                        UpdatePhoneResponse updateResponse = JsonConvert.DeserializeObject<UpdatePhoneResponse>(response.Content);
                        ro.Responses.Add(new ResponseDetails()
                        {
                            URL = response.ResponseUri.ToString(),
                            SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                            ResponseContent = response.Content
                        });
                    }
                }
            }
        }
        
        private ResultsObject CreateNewPhoneNumber(ResultsObject ro, Enums.Enums.SendRequest requestType, string changedValue)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            if(!string.IsNullOrEmpty(personId) && !string.IsNullOrEmpty(changedValue))
            {
                IRestRequest request = new RestRequest("/person/phone_numbers/", Method.POST);
                request.AddHeader("Authentication-Token", _apiToken);
                request = RequestBodyHelper.CreatePhoneNumberBody(changedValue, personId, requestType, request);

                if (_appSettings.RunAsConsole)
                { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                CreatePhoneNumberResponse createPhoneNumberResponse = JsonConvert.DeserializeObject<CreatePhoneNumberResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails()
                {
                    SendRequest = requestType,
                    ChangedValue = changedValue,
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
            }
            return ro;
        }

        private List<PhoneNumberResponse> GetPhoneNumbers(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<PhoneNumberResponse> phoneNumbers = new List<PhoneNumberResponse>();
            if(!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/person/phone_numbers?person_id=" + personId);
                request.AddHeader("Authentication-Token", _apiToken);

                if (_appSettings.RunAsConsole)
                { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                phoneNumbers = JsonConvert.DeserializeObject<List<PhoneNumberResponse>>(response.Content);
                ro.Responses.Add(new ResponseDetails()
                {
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
            }
            return phoneNumbers;
        }

        private void UpdateCurrentEmailsToNonActive(ResultsObject ro, string changedValue)
        {
            List<EmailResponse> emailAddresses = GetEmailAddresses(ro);
            if(emailAddresses.Count>0)
            {
                foreach(EmailResponse address in emailAddresses)
                {
                    if (address.Email_address != changedValue)
                    {
                        IRestRequest request = new RestRequest("/person/email_addresses/" + address.Id, Method.PATCH);
                        request.AddHeader("Authentication-Token", _apiToken);
                        request = RequestBodyHelper.CreateUpdateEmailAddressToNonActiveBody(request);

                        if (_appSettings.RunAsConsole)
                        { Console.WriteLine("Sending: {0}\n", request.Resource); }
                        IRestResponse response = _restClient.Execute(request);
                        if (_appSettings.RunAsConsole)
                        {
                            Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                            Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                        }

                        UpdateEmailResponse updateResponse = JsonConvert.DeserializeObject<UpdateEmailResponse>(response.Content);
                        ro.Responses.Add(new ResponseDetails()
                        {
                            URL = response.ResponseUri.ToString(),
                            SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                            ResponseContent = response.Content
                        });
                    }
                }
            }
        }

        private List<EmailResponse> GetEmailAddresses(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<EmailResponse> emailResponses = new List<EmailResponse>();
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/person/email_addresses?person_id=" + personId);
                request.AddHeader("Authentication-Token", _apiToken);

                if (_appSettings.RunAsConsole)
                { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                emailResponses = JsonConvert.DeserializeObject<List<EmailResponse>>(response.Content);
                ro.Responses.Add(new ResponseDetails() { URL = response.ResponseUri.ToString(),
                                                            SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                                                            ResponseContent = response.Content
                });
            }
            return emailResponses;
        }

        private ResultsObject CreateNewEmailAddress(ResultsObject ro, Enums.Enums.SendRequest requestType, string changedValue)
        {
            string personId = GetPersonId(ro.ReferenceNumber);            
            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest("/person/email_addresses/", Method.POST);
                request.AddHeader("Authentication-Token", _apiToken);

                request = RequestBodyHelper.CreateEmailAddressBody(changedValue, personId, requestType, request);

                if (_appSettings.RunAsConsole)
                { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                CreateEmailResponse createEmailResponse = JsonConvert.DeserializeObject<CreateEmailResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails()
                {
                    SendRequest = requestType,
                    ChangedValue = changedValue,
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
            }
            return ro;
        }
    }
}
