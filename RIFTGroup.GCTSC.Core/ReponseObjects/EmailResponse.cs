using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class EmailResponse
    {
        public string Id { get; set; }
        public string Person_id { get; set; }
        public string Email_address { get; set; }
        public string Active { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }

    }
}
