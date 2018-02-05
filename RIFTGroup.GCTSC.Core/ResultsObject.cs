﻿using System;
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
        public Dictionary<string, Enums.Enums.SendResponse> SendResponses { get; set; }
        public string ChangedValue { get; set; }
    }
}
