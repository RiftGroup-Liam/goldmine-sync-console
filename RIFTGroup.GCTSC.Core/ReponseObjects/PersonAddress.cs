using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class PersonAddress
    {
        public string id { get; set; }
        public string person_id { get; set; }
        public string address_id { get; set; }
        public DateTime started_at { get; set; }
        public DateTime? ended_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool Active
        {
            get
            { return ended_at == null; }
        }
    }
}
