using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class ClaimRefund
    {
        public int id { get; set; }
        public int person_claim_id { get; set; }
        public int refund_owner_id { get; set; }
        public string service_type { get; set; }
        public string claim_type { get; set; }
        public string expected_refund { get; set; }
        public string expected_fee { get; set; }
        public string actual_refund { get; set; }
        public string actual_fee { get; set; }
        public DateTime? expected_refund_updated_at { get; set; }
        public bool customer_signed_claim { get; set; }
        public DateTime? completed_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
