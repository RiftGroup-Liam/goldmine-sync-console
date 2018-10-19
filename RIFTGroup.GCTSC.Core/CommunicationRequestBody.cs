using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public class CommunicationRequestBody
    {
        public string person_id { get; set; }
        public bool? permitted { get; set; }
        public string name { get; set; }
    }
}
