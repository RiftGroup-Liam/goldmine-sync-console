using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class CreatePersonAddressResponse
    {
        public string id { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public string first_line { get; set; }
        public string second_line { get; set; }
        public string town { get; set; }
        public string postcode { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
