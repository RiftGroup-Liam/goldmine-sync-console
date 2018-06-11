using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class UpdateClaimStatusResponse
    {
        public string Id { get; set; }
        public string Person_id { get; set; }
        public string Claim_status_id { get; set; }
        public string Year { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Claim_sub_status_id { get; set; }
    }
}
