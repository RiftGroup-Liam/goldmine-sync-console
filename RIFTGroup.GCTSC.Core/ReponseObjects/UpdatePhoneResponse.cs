﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class UpdatePhoneResponse
    {
        public string Id { get; set; }
        public string Person_id { get; set; }
        public string Country_code { get; set; }
        public string Subscriber_number { get; set; }
        public bool Active { get; set; } 
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
