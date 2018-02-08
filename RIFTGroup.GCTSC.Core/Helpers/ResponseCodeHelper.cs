using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Helpers
{
    public static class ResponseCodeHelper
    {
        public static Enums.Enums.SendResponse TranslateResponseCode(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.OK)
                return Enums.Enums.SendResponse.OK;
            else if (statusCode == HttpStatusCode.NotFound)
                return Enums.Enums.SendResponse.NOT_FOUND;
            else if (statusCode == HttpStatusCode.Unauthorized)
                return Enums.Enums.SendResponse.UNAUTHORISED;
            else
                return Enums.Enums.SendResponse.FAILED;
        }
    }
}
