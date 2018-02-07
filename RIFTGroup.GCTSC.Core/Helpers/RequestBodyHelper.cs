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
            if(requestType == Enums.Enums.SendRequest.CONTACT)
            {
                restRequest.AddParameter("tbc",  changedValue);
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
            if (requestType == Enums.Enums.SendRequest.KEY2)
            {
                restRequest.AddParameter("tbc", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.UCLIENTSTA)
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

        public static IRestRequest CreateUpdateEmailAddressToNonActiveBody(string changedValue, IRestRequest request)
        {
            request.AddParameter("active", "false");
            return request;
        }

        public static IRestRequest CreateEmailAddressBody(string changedValue, string person_id, IRestRequest request)
        {
            request.AddParameter("person_id", person_id);
            request.AddParameter("email_address", changedValue);
            request.AddParameter("active", "true");
            return request;
        }
    }
}
