using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class CreatePersonResponse
    {
        public string Id { get; set; }
        public string Goldmine_customer_number { get; set; }
        public string First_name { get; set; }
        public string Middle_names { get; set; }
        public string Last_name { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
