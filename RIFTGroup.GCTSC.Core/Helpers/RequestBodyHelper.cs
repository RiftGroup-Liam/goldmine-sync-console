using Newtonsoft.Json;
using RestSharp;
using RIFTGroup.GCTSC.Core.Model;
using System;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public class RequestBodyHelper
    {
        public static IRestRequest CreateCaseOwnerBody(string changedValue, IRestRequest request)
        {
            request.AddParameter("name", changedValue);
            return request;
        }

        public static IRestRequest CreateClaimYearBody(IRestRequest request, string personId, Enums.Enums.Year year)
        {
            request.AddParameter("person_id", personId);
            request.AddParameter("year", ((int)year).ToString());
            request.AddParameter("claim_status_id", "8");
            return request;
        }

        public static IRestRequest CreateCommunicationPreferenceBody(Enums.Enums.CommPreferenceType type, bool? changedValue, IRestRequest request, string person_id)
        {
            CommunicationRequestBody body = new CommunicationRequestBody()
            {
                person_id = person_id,
                name = type.ToString().ToLower(),
                permitted = changedValue
            };

            request.AddJsonBody(body);
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

            if (requestType == Enums.Enums.SendRequest.PHONE1)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.Mobile);
            }
            else if (requestType == Enums.Enums.SendRequest.PHONE2)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.Home);
            }
            else if (requestType == Enums.Enums.SendRequest.PHONE3)
            {
                request.AddParameter("phone_number_kind_id", (int)Enums.Enums.PhoneNumberKind.General);
            }
            return request;
        }

        public static IRestRequest CreateUpdateClaimStatusBody(IRestRequest request, string existingYearId, int statusID)
        {
            request.AddParameter("id", existingYearId);
            request.AddParameter("claim_status_id", statusID.ToString());
            return request;
        }

        public static IRestRequest CreateUpdateEmailAddressToNonActiveBody(IRestRequest request)
        {
            request.AddParameter("active", "false");
            return request;
        }

        public static IRestRequest CreateUpdatePersonRequestBody(Enums.Enums.SendRequest requestType, string changedValue, IRestRequest restRequest)
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
            if (requestType == Enums.Enums.SendRequest.UBCASEOWN)
            {
                restRequest.AddParameter("case_owner_id", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.UTR)
            {
                restRequest.AddParameter("unique_tax_reference", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.NINO)
            {
                restRequest.AddParameter("national_insurance_number", changedValue);
            }
            if (requestType == Enums.Enums.SendRequest.DOB)
            {
                restRequest.AddParameter("date_of_birth", changedValue);
            }
            if(requestType == Enums.Enums.SendRequest.REPLEAD)
            {
                restRequest.AddParameter("rep_lead", changedValue);
            }
            return restRequest;
        }

        public static IRestRequest CreateAddressRequestBody(IRestRequest request, Address address)
        {
            request.AddParameter("first_line", address.first_line);
            request.AddParameter("second_line", address.second_line);
            request.AddParameter("town", address.town);
            request.AddParameter("county", address.county);
            request.AddParameter("country", address.country);
            request.AddParameter("postcode", address.postcode);
            return request;
        }

        public static IRestRequest CreateUpdateAddressRequestBody(Address address, IRestRequest request)
        {
            request.AddParameter("first_line", address.first_line);
            request.AddParameter("second_line", address.second_line);
            request.AddParameter("town", address.town);
            request.AddParameter("county", address.county);
            request.AddParameter("country", address.country);
            request.AddParameter("postcode", address.postcode);
            return request;
        }

        public static IRestRequest CreateUpdateAddressToInactiveRequestBody(IRestRequest request)
        {
            request.AddParameter("ended_at", DateTime.Now.ToString());
            return request;
        }

        public static IRestRequest CreatePersonAddressRequestBody(string addressId, string personId, IRestRequest request)
        {
            request.AddParameter("person_id", personId);
            request.AddParameter("address_id", addressId);
            request.AddParameter("started_at", DateTime.Now.ToString("yyyy-MM-dd"));
            return request;
        }

        public static IRestRequest CreateUpdatePhoneNumberToNonActiveRequestBody(IRestRequest request)
        {
            request.AddParameter("active", "false");
            return request;
        }

        public static IRestRequest UpdateCommunicationPreferenceBody(Enums.Enums.CommPreferenceType type, bool? changedValue, IRestRequest request, string personId, string existingPreferenceId)
        {
            CommunicationRequestBody body = new CommunicationRequestBody()
            {
                person_id = personId,
                name = type.ToString().ToLower(),
                permitted = changedValue
            };

            request.AddJsonBody(body);
            return request;
        }

        public static IRestRequest UpdateRefundBody(IRestRequest request, string existingRefundId, string claimId, string caseownerId, Refund refund)
        {
            request.AddParameter("id", existingRefundId);
            request.AddParameter("person_claim_id", claimId);
            request.AddParameter("refund_owner_id", caseownerId);
            request.AddParameter("service_type", refund.ServiceType);
            request.AddParameter("claim_type", refund.Type);
            request.AddParameter("expected_refund", refund.ExpectedRefund);
            request.AddParameter("expected_fee", refund.ExpectedFee);
            request.AddParameter("actual_refund", refund.ActualRefund);
            request.AddParameter("actual_fee", refund.ActualFee);
            request.AddParameter("customer_signed_claim", refund.Signed);
            if (refund.CompletedAt.HasValue)
            {
                request.AddParameter("completed_at", refund.CompletedAt.Value.ToString("yyyy-MM-dd"));
            }
            return request;
        }

        public static IRestRequest CreateRefundBody(IRestRequest request, string claimId, string caseownerId, Refund refund)
        {
            request.AddParameter("person_claim_id", claimId);
            request.AddParameter("refund_owner_id", caseownerId);
            request.AddParameter("service_type", refund.ServiceType);
            request.AddParameter("claim_type", refund.Type);
            request.AddParameter("expected_refund", refund.ExpectedRefund);
            request.AddParameter("expected_fee", refund.ExpectedFee);
            request.AddParameter("actual_refund", refund.ActualRefund);
            request.AddParameter("actual_fee", refund.ActualFee);
            request.AddParameter("customer_signed_claim", refund.Signed);
            if (refund.CompletedAt.HasValue)
            {
                request.AddParameter("completed_at", refund.CompletedAt.Value.ToString("yyyy-MM-dd"));
            }
            return request;
        }
    }
}