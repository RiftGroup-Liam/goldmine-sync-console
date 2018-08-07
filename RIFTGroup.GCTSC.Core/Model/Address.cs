using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Model
{
    public class Address
    {
        public string first_line { get; set; }
        public string second_line { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public string town { get; set; }
    }
}
