using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public class ResponseDetails
    {
        public string URL { get; set; }
        public Enums.Enums.SendResponse SendResponse {get;set;}
        public string ChangedValue { get; set; }
        public Enums.Enums.SendRequest SendRequest { get; set; }
    }
}
