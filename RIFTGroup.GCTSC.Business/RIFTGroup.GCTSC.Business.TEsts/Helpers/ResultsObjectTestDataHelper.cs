using RIFTGroup.GCTSC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business.Tests.Helpers
{
    public class ResultsObjectTestDataHelper
    {
        public string _testAccountno;
        public ResultsObjectTestDataHelper()
        {
            _testAccountno = "B5040182438 F[6CPLia";
        }

        public ResultsObject CreateTestData()
        {
            ResultsObject ro = new ResultsObject();
            ro.Accountno = _testAccountno;
            ro.ReferenceNumber = "165992";
            ro.Responses = new List<ResponseDetails>();
            ro.Responses.Add(new ResponseDetails()
            {
                ChangedValue = "Arnold",
                SendRequest = Core.Enums.Enums.SendRequest.LASTNAME,
                SendResponse = Core.Enums.Enums.SendResponse.OK,
                URL = "http://rift-data-api-staging.herokuapp.com:80/people",
                ResponseContent = @"{""id"":1,""first_name"":""TestChange"",""goldmine_customer_number"":165992,""middle_names"":null,""last_name"":""Arnold"",""created_at"":""2018 - 02 - 06T10:29:13.835Z"",""updated_at"":""2018 - 02 - 07T08:03:59.298Z""}"
            });
            ro.Responses.Add(new ResponseDetails()
            {
                ChangedValue = "",
                SendRequest = 0,
                SendResponse = Core.Enums.Enums.SendResponse.OK,
                URL = "http://rift-data-api-staging.herokuapp.com:80//person/email_addresses?person_id=1",
                ResponseContent = @"[{""id"": 1, ""person_id"": 1, ""email_address"": ""liamarnold93@gmail.com"", ""active"": false, ""created_at"": ""2018-02-06T12:40:00.355Z"", ""updated_at"": ""2018-02-07T08:58:42.808Z""}]"
            });
            return ro;
        }
    }
}
