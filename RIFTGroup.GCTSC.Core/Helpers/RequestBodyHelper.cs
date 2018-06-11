﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RIFTGroup.GCTSC.Core.Enums;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public class RequestBodyHelper
    {
        public static IRestRequest CreateUpdatePersonRequestBody(Enums.Enums.SendRequest requestType,
                                                                    string changedValue, IRestRequest restRequest)
        {
            if (requestType == Enums.Enums.SendRequest.CONTACT)
            {
                restRequest.AddParameter("first_name", NameHelper.CreateFirstnameFromContact(changedValue));
                restRequest.AddParameter("last_name", NameHelper.CreateLastnameFromContact(changedValue));
                restRequest.AddParameter("middle_names", NameHelper.CreateMiddleNamesFromContact(changedValue));
            }
            if (requestType == Enums.Enums.SendRequest.SECR)
            {
                restRequest.AddParameter("first_name", NameHelper.CreateFirstnameFromContact(changedValue));
            }
            if (requestType == Enums.Enums.SendRequest.LASTNAME)
            {
                restRequest.AddParameter("last_name", NameHelper.CreateLastnameFromContact(changedValue));
            }
            if (requestType == Enums.Enums.SendRequest.KEY5)
            {
                restRequest.AddParameter("goldmine_customer_number", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.UCONVDATE)
            {
                restRequest.AddParameter("authorized_at", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.USTAGE1DAT)
            {
                restRequest.AddParameter("qualified_at", changedValue);
            }
            if(requestType == Enums.Enums.SendRequest.UBCASEOWN)
            {
                restRequest.AddParameter("case_owner_id", changedValue);
            }
            return restRequest;
        }

        public static IRestRequest CreateUpdateEmailAddressToNonActiveBody(IRestRequest request)
        {
            request.AddParameter("active", "false");
            return request;
        }

        public static IRestRequest CreateUpdatePhoneNumberToNonActiveRequestBody(IRestRequest request)
        {
            request.AddParameter("active", "false");
            return request;
        }

        public static IRestRequest CreateEmailAddressBody(string changedValue, string person_id,
                                                            Enums.Enums.SendRequest requestType, IRestRequest request)
        {
            request.AddParameter("person_id", person_id);
            request.AddParameter("email_address", changedValue);
            request.AddParameter("active", "true");
            return request;
        }

        public static IRestRequest CreatePersonRequestBody(IRestRequest request, ClientData clientData)
        {
            request.AddParameter("goldmine_customer_number", clientData.Key5);
            request.AddParameter("first_name", NameHelper.CreateFirstnameFromContact(clientData.Secr));
            request.AddParameter("last_name", NameHelper.CreateLastnameFromContact(clientData.Lastname));
            return request;
        }

        public static IRestRequest CreatePhoneNumberBody(string changedValue, string personId, 
                                                            Enums.Enums.SendRequest requestType, IRestRequest request)
        {
            request.AddParameter("person_id", personId);
            request.AddParameter("country_code", "+44");
            request.AddParameter("subscriber_number", PhoneNumberHelper.CreateSubscriberNumber(changedValue)); 
            request.AddParameter("active", "true");

            if(requestType == Enums.Enums.SendRequest.PHONE1)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.Mobile);
            }
            else if(requestType == Enums.Enums.SendRequest.PHONE2)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.Home);
            }
            else if (requestType == Enums.Enums.SendRequest.PHONE3)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.General);
            }
            return request;
        }

        public static IRestRequest UpdateCommunicationPreferenceBody(Enums.Enums.CommPreferenceType type, bool changedValue, IRestRequest request, string personId, string existingPreferenceId)
        {
            request.AddParameter("person_id", personId);
            request.AddParameter("permitted", changedValue.ToString().ToLower());
            request.AddParameter("name", type.ToString().ToLower());
            return request;
        }

        public static IRestRequest CreateCaseOwnerBody(string changedValue, IRestRequest request)
        {
            request.AddParameter("name", changedValue);
            return request;
        }

        public static IRestRequest CreateCommunicationPreferenceBody(Enums.Enums.CommPreferenceType type, bool changedValue, IRestRequest request, string person_id)
        {
            request.AddParameter("person_id", person_id);
            request.AddParameter("permitted", changedValue.ToString().ToLower());
            request.AddParameter("name", type.ToString().ToLower());
            return request;
        }

        public static IRestRequest CreateUpdateClaimStatusBody(IRestRequest request, string existingYearId)
        {
            request.AddParameter("id", existingYearId);
            request.AddParameter("claim_status_id", "8");
            return request;
        }

        public static IRestRequest CreateClaimYearBody(IRestRequest request, string personId, Enums.Enums.Year year)
        {
            request.AddParameter("person_id", personId);
            request.AddParameter("year", ((int)year).ToString());
            request.AddParameter("claim_status_id", "8");
            return request;
        }
    }
}
