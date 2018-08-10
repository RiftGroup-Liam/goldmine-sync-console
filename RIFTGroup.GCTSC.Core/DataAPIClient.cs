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
using System.Web;
using RIFTGroup.GCTSC.Core.Model;

namespace RIFTGroup.GCTSC.Core
{
    public class DataAPIClient
    {
        private string _apiToken;
        private AppSettings _appSettings;
        private string _baseAddress;
        private RestClient _restClient;

        public DataAPIClient()
        {
            _appSettings = new AppSettings();
            _baseAddress = _appSettings.ApiURL;
            _apiToken = _appSettings.APIToken;
            _restClient = new RestClient(_baseAddress);
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

            CreatePersonResponse creatgePersonResponse = JsonConvert.DeserializeObject<CreatePersonResponse>(response.Content);
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

        public ResultsObject SendUpdateUTRRequest(ResultsObject ro, string changedValue)
        {
            ro = SendUpdatePersonRequest(Enums.Enums.SendRequest.UTR, ro, changedValue);
            return ro;
        }

        public ResultsObject SendUpdateNINORequest(ResultsObject ro, string changedValue)
        {
            ro = SendUpdatePersonRequest(Enums.Enums.SendRequest.NINO, ro, changedValue);
            return ro;
        }

        public ResultsObject SendUpdateAllClaimsStatus(ResultsObject ro, int statusID)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<ClaimYearResponse> claims = GetPersonsClaims(personId);
            foreach (var claim in claims)
            {
                int currentStatusID;
                bool result = int.TryParse(claim.Claim_status_id, out currentStatusID);
                if (result && currentStatusID <= statusID)
                {
                    ro = SendUpdateClaimStatusRequest(ro, claim.Id, statusID);
                }
            }

            return ro;
        }

        public bool CheckForOthersLinkedToThisAddress(string dataapi_addressId)
        {
            bool othersLinkedToThisAddress = true;
            IRestRequest request = new RestRequest(
               string.Format("/person/addresses?address_id=" + dataapi_addressId)
            );
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
                List<PersonAddress> addressResponse = JsonConvert.DeserializeObject<List<PersonAddress>>(response.Content);
                if (addressResponse.Count <= 1)
                {
                    othersLinkedToThisAddress = false;
                }
            }
            return othersLinkedToThisAddress;
        }

        public bool CheckIfImLinkedToAddress(string dataapi_addressId, string personId)
        {
            bool imLinkedToAddress = false;
            IRestRequest request = new RestRequest(
               string.Format("/person/addresses?address_id=" + dataapi_addressId)
            );
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
                List<PersonAddress> addressResponse = JsonConvert.DeserializeObject<List<PersonAddress>>(response.Content);
                foreach(PersonAddress personAddress in addressResponse)
                {
                    if(personAddress.person_id == personId)
                    {
                        imLinkedToAddress = true;
                    }
                }
            }
            return imLinkedToAddress;
        }

        public string CreateAddress(Address address, ResultsObject ro)
        {
            string newAddressId = string.Empty;
            IRestRequest request = new RestRequest("/addresses", Method.POST);
            request.AddHeader("Authentication-Token", _apiToken);

            request = RequestBodyHelper.CreateAddressRequestBody(request, address);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            CreateAddressResponse createAddressReponse = JsonConvert.DeserializeObject<CreateAddressResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                SendRequest = Enums.Enums.SendRequest.CREATEADDRESS,
                ChangedValue = "CREATE",
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            newAddressId = createAddressReponse.id;
            return newAddressId;
        }

        public string CreatePersonAddress(string addressId, string personId, ResultsObject ro)
        {
            string personAddressId = string.Empty;
            IRestRequest request = new RestRequest("/person/addresses", Method.POST);
            request.AddHeader("Authentication-Token", _apiToken);

            request = RequestBodyHelper.CreatePersonAddressRequestBody(addressId, personId, request);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            CreatePersonAddressResponse createPersonAddressReponse = JsonConvert.DeserializeObject<CreatePersonAddressResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                SendRequest = Enums.Enums.SendRequest.CREATEPERSONADDRESS,
                ChangedValue = "CREATE",
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            if(createPersonAddressReponse != null)
            {
                personAddressId = createPersonAddressReponse.id;
            }
            return personAddressId;
        }

        public void SetOtherAddressesToInactive(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<PersonAddress> personAddresses = GetOtherPersonAddresses(personId);
            foreach(PersonAddress personAddress in personAddresses)
            {
                if(personAddress.Active)
                {
                    SetAddresstoInactive(personAddress.id, ro);
                }
            }
        }

        private ResultsObject SetAddresstoInactive(string id, ResultsObject ro)
        {
            IRestRequest request = new RestRequest(
               string.Format("/person/addresses/" + id),
               Method.PATCH
            );
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.CreateUpdateAddressToInactiveRequestBody(request);

            if (_appSettings.RunAsConsole) { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            UpdateAddressResponse updateResponse = JsonConvert.DeserializeObject<UpdateAddressResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                SendRequest = Enums.Enums.SendRequest.ADDRESS,
                ChangedValue = "address",
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            return ro;
        }

        public bool CheckForOtherActiveAddresses(string personId)
        {
            bool otherActiveAddresses = false;
            IRestRequest request = new RestRequest(
               string.Format("/person/addresses?person_id=" + personId)
            );
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
                List<PersonAddress> addressResponse = JsonConvert.DeserializeObject<List<PersonAddress>>(response.Content);
                if (addressResponse != null)
                {
                    otherActiveAddresses = addressResponse.HasActiveAddresses();
                }
            }
            return otherActiveAddresses;
        }

        public List<PersonAddress> GetOtherPersonAddresses(string personId)
        {
            List<PersonAddress> personAddresses = new List<PersonAddress>();
            IRestRequest request = new RestRequest(
               string.Format("/person/addresses?person_id=" + personId)
            );
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
                personAddresses = JsonConvert.DeserializeObject<List<PersonAddress>>(response.Content);
            }
            return personAddresses;
        }

        public ResultsObject AmendCurrentAddress(Address address, ResultsObject ro, string dataapi_addressId)
        {
            IRestRequest request = new RestRequest(
               string.Format("/addresses/" + dataapi_addressId),
               Method.PATCH
            );
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.CreateUpdateAddressRequestBody(address, request);

            if (_appSettings.RunAsConsole) { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            UpdateAddressResponse updateResponse = JsonConvert.DeserializeObject<UpdateAddressResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                SendRequest = Enums.Enums.SendRequest.ADDRESS,
                ChangedValue = "address",
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            return ro;
        }

        public string GetExistingAddress(string first_line, string postcode)
        {
            string addressId = string.Empty;
            IRestRequest request = new RestRequest(
                string.Format("/addresses?postcode={0}&first_line={1}", HttpUtility.UrlEncode(postcode), HttpUtility.UrlEncode(first_line))
            );
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
                List<AddressResponse> addressResponse = JsonConvert.DeserializeObject<List<AddressResponse>>(response.Content);
                if (addressResponse.Count != 0)
                {
                    addressId = addressResponse[0].id;
                }
            }

            return addressId;
        }

        public ResultsObject SendUpdateCaseOwnerRequest(Enums.Enums.SendRequest ubcaseown, ResultsObject ro, string changedValue)
        {
            string caseownerId = GetCaseownerId(changedValue);
            if (caseownerId == "")
            {
                caseownerId = CreateCaseOwnerRequest(changedValue, ubcaseown, ro);
            }
            ro = SendUpdatePersonRequest(ubcaseown, ro, caseownerId.ToString());
            return ro;
        }

        public ResultsObject SendUpdateClaimStatus(Enums.Enums.Year year, ResultsObject ro, int statusID)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            string existingYearId = GetCurrentClaimYear(year, personId, ro);
            if (!string.IsNullOrEmpty(existingYearId))
            {
                ro = SendUpdateClaimStatusRequest(ro, existingYearId, statusID);
            }
            else
            {
                ro = CreateClaimYear(year, personId, ro);
            }
            return ro;
        }

        public ResultsObject SendUpdateCommunicationPreference(Enums.Enums.CommPreferenceType type, ResultsObject ro, bool changedValue)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            string existingCommunicationPreferenceId = GetCurrentCommunicationPreference(type, personId, ro);
            if (!string.IsNullOrEmpty(existingCommunicationPreferenceId))
            {
                SendUpdateCommunicationPreferenceRequest(type, ro, changedValue, personId, existingCommunicationPreferenceId);
            }
            else
            {
                CreateCommunicationPreference(type, personId, ro, changedValue);
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

        public ResultsObject SendUpdateDOBRequest(ResultsObject ro, string changedValue)
        {
            ro = SendUpdatePersonRequest(Enums.Enums.SendRequest.DOB, ro, changedValue);
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

        private string CreateCaseOwnerRequest(string changedValue, Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            string newCaseOwnerId = string.Empty;
            if (!string.IsNullOrEmpty(changedValue))
            {
                IRestRequest request = new RestRequest("/case_owners", Method.POST);
                request.AddHeader("Authentication-Token", _apiToken);
                request = RequestBodyHelper.CreateCaseOwnerBody(changedValue, request);

                if (_appSettings.RunAsConsole)
                { Console.WriteLine("Sending: {0}\n", request.Resource); }
                IRestResponse response = _restClient.Execute(request);
                if (_appSettings.RunAsConsole)
                {
                    Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                    Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
                }

                CreateCaseOwnerResponse createCaseownerResponse = JsonConvert.DeserializeObject<CreateCaseOwnerResponse>(response.Content);
                ro.Responses.Add(new ResponseDetails()
                {
                    SendRequest = requestType,
                    ChangedValue = changedValue,
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
                if (createCaseownerResponse != null) { newCaseOwnerId = createCaseownerResponse.Id; }
            }
            return newCaseOwnerId;
        }

        private ResultsObject CreateClaimYear(Enums.Enums.Year year, string personId, ResultsObject ro)
        {
            IRestRequest request = new RestRequest("/person/claims", Method.POST);
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.CreateClaimYearBody(request, personId, year);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            CreateClaimYearResponse createResponse = JsonConvert.DeserializeObject<CreateClaimYearResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            return ro;
        }

        private string CreateCommunicationPreference(Enums.Enums.CommPreferenceType type, string personId, ResultsObject ro, bool changedValue)
        {
            string newCommunicationPreferenceId = string.Empty;
            IRestRequest request = new RestRequest("/person/communication_preferences", Method.POST);
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.CreateCommunicationPreferenceBody(type, changedValue, request, personId);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            CreateCommunicationPreferenceResponse createCommunicationPreferenceResponse = JsonConvert.DeserializeObject<CreateCommunicationPreferenceResponse>(response.Content);
            ResponseDetails responseDetails = new ResponseDetails()
            {
                ChangedValue = changedValue.ToString(),
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            };
            switch (type)
            {
                case Enums.Enums.CommPreferenceType.Email:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.EMAILPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.Phone:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.PHONEPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.Post:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.POSTPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.SMS:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.SMSPREFERENCE;
                    break;
            }
            ro.Responses.Add(responseDetails);
            if (createCommunicationPreferenceResponse != null) { newCommunicationPreferenceId = createCommunicationPreferenceResponse.Id; }

            return newCommunicationPreferenceId;
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

        private ResultsObject CreateNewPhoneNumber(ResultsObject ro, Enums.Enums.SendRequest requestType, string changedValue)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            if (!string.IsNullOrEmpty(personId) && !string.IsNullOrEmpty(changedValue))
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

        private string GetCaseownerId(string changedValue)
        {
            string caseownerId = string.Empty;

            IRestRequest request = new RestRequest("/case_owners?name=" + changedValue);
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
                List<CaseOwnerResponse> caseownerResponse = JsonConvert.DeserializeObject<List<CaseOwnerResponse>>(response.Content);
                if (caseownerResponse.Count != 0)
                {
                    caseownerId = caseownerResponse[0].Id;
                }
            }

            return caseownerId;
        }

        private string GetCurrentClaimYear(Enums.Enums.Year year, string personId, ResultsObject ro)
        {
            string currentClaimYearId = string.Empty;
            IRestRequest request = new RestRequest(string.Format("/person/claims?person_id={0}&year={1}", personId, ((int)year).ToString()));
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
                List<ClaimYearResponse> claimYearResponse = JsonConvert.DeserializeObject<List<ClaimYearResponse>>(response.Content);
                if (claimYearResponse.Count != 0)
                {
                    currentClaimYearId = claimYearResponse[0].Id;
                }
            }

            return currentClaimYearId;
        }

        private string GetCurrentCommunicationPreference(Enums.Enums.CommPreferenceType type, string personId, ResultsObject ro)
        {
            string existingCommunicationPreferenceId = string.Empty;
            IRestRequest request = new RestRequest(string.Format("person/communication_preferences?person_id={0}&name={1}", personId, type.ToString().ToLower()));
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
                List<CommunicationPreferenceResponse> communicationPreferenceResponse = JsonConvert.DeserializeObject<List<CommunicationPreferenceResponse>>(response.Content);
                if (communicationPreferenceResponse.Count != 0)
                {
                    existingCommunicationPreferenceId = communicationPreferenceResponse[0].Id;
                }
                ro.Responses.Add(new ResponseDetails()
                {
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
            }

            return existingCommunicationPreferenceId;
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
                ro.Responses.Add(new ResponseDetails()
                {
                    URL = response.ResponseUri.ToString(),
                    SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                    ResponseContent = response.Content
                });
            }
            return emailResponses;
        }

        private List<ClaimYearResponse> GetPersonsClaims(string personId)
        {
            List<ClaimYearResponse> claims = new List<ClaimYearResponse>();

            if (!string.IsNullOrEmpty(personId))
            {
                IRestRequest request = new RestRequest(string.Format("/person/claims?person_id={0}", personId));
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
                    claims = JsonConvert.DeserializeObject<List<ClaimYearResponse>>(response.Content);
                }
            }

            return claims;
        }

        private List<PhoneNumberResponse> GetPhoneNumbers(ResultsObject ro)
        {
            string personId = GetPersonId(ro.ReferenceNumber);
            List<PhoneNumberResponse> phoneNumbers = new List<PhoneNumberResponse>();
            if (!string.IsNullOrEmpty(personId))
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

        private ResultsObject SendUpdateClaimStatusRequest(ResultsObject ro, string existingYearId, int statusID)
        {
            IRestRequest request = new RestRequest(string.Format("/person/claims/{0}", existingYearId), Method.PATCH);
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.CreateUpdateClaimStatusBody(request, existingYearId, statusID);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            UpdateClaimStatusResponse updateResponse = JsonConvert.DeserializeObject<UpdateClaimStatusResponse>(response.Content);
            ro.Responses.Add(new ResponseDetails()
            {
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            });
            return ro;
        }

        private void SendUpdateCommunicationPreferenceRequest(Enums.Enums.CommPreferenceType type, ResultsObject ro, bool changedValue, string personId, string existingPreferenceId)
        {
            IRestRequest request = new RestRequest(string.Format("/person/communication_preferences/{0}", existingPreferenceId), Method.PATCH);
            request.AddHeader("Authentication-Token", _apiToken);
            request = RequestBodyHelper.UpdateCommunicationPreferenceBody(type, changedValue, request, personId, existingPreferenceId);

            if (_appSettings.RunAsConsole)
            { Console.WriteLine("Sending: {0}\n", request.Resource); }
            IRestResponse response = _restClient.Execute(request);
            if (_appSettings.RunAsConsole)
            {
                Console.WriteLine("Response Status: {0}\n", response.StatusCode);
                Console.WriteLine("Response URL: {0}\n", response.ResponseUri);
            }

            UpdateCommunicationPreferenceResponse updateCommunicationPreferenceResponse = JsonConvert.DeserializeObject<UpdateCommunicationPreferenceResponse>(response.Content);
            ResponseDetails responseDetails = new ResponseDetails()
            {
                ChangedValue = changedValue.ToString(),
                URL = response.ResponseUri.ToString(),
                SendResponse = ResponseCodeHelper.TranslateResponseCode(response.StatusCode),
                ResponseContent = response.Content
            };
            switch (type)
            {
                case Enums.Enums.CommPreferenceType.Email:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.EMAILPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.Phone:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.PHONEPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.Post:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.POSTPREFERENCE;
                    break;

                case Enums.Enums.CommPreferenceType.SMS:
                    responseDetails.SendRequest = Enums.Enums.SendRequest.SMSPREFERENCE;
                    break;
            }
            ro.Responses.Add(responseDetails);
        }

        private void UpdateCurrentEmailsToNonActive(ResultsObject ro, string changedValue)
        {
            List<EmailResponse> emailAddresses = GetEmailAddresses(ro);
            if (emailAddresses.Count > 0)
            {
                foreach (EmailResponse address in emailAddresses)
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

        private void UpdateCurrentPhoneNumbersToNonActive(ResultsObject ro, string changedValue)
        {
            List<PhoneNumberResponse> phoneNumbers = GetPhoneNumbers(ro);
            if (phoneNumbers.Count > 0)
            {
                foreach (PhoneNumberResponse phoneNumber in phoneNumbers)
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
    }
}