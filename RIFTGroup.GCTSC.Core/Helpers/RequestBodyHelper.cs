using System;
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
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.SECR)
            {
                restRequest.AddParameter("first_name", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.LASTNAME)
            {
                restRequest.AddParameter("last_name", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.KEY5)
            {
                restRequest.AddParameter("goldmine_customer_number", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.UCONVDATE)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.USTAGE1DAT)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            return restRequest;
        }

        public static IRestRequest CreateEmailAddressUpdateRequestBody(Enums.Enums.SendRequest requestType,
                                                                        string changedValue, IRestRequest restRequest)
        {
            if (requestType == Enums.Enums.SendRequest.UEMAILADDR)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.CONTSUPREF)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            return restRequest;
        }

        public static IRestRequest CreatePhoneNumberUpdateRequestBody(Enums.Enums.SendRequest requestType,
                                                                        string changedValue, IRestRequest restRequest)
        {
            if (requestType == Enums.Enums.SendRequest.PHONE1)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.PHONE2)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.PHONE3)
            {
                restRequest.AddParameter("tbc", changedValue);
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
            request.AddParameter("first_name", clientData.Secr);
            request.AddParameter("last_name", clientData.Lastname);
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
    }
}
