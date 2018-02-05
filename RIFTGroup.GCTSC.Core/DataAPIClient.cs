using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core.Enums;
using RestSharp;

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
            return ro;
        }

        public ResultsObject SendUpdateEmailAddressRequest(Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            return ro;
        }

        public ResultsObject SendUpdatePhoneNumberRequest(Enums.Enums.SendRequest requestType, ResultsObject ro)
        {
            return ro;
        }

        private ResultsObject SendDataApiRequest(RestRequest request, ResultsObject ro)
        {
            return ro;
        }

        private string GetPersonId(string referenceNumber)
        {
            string personId = string.Empty;

            return personId;
        }
    }
}
