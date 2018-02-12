using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core.Enums;

namespace RIFTGroup.GCTSC.Core
{
    public class ResultsObject
    {
        public string Accountno { get; set; }
        public string ReferenceNumber { get; set; }
        public List<ResponseDetails> Responses{ get; set; }
    }
}
