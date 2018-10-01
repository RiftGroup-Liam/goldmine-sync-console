using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Model
{
    public class Refund
    {
        public int Year { get; set; }
        public string ServiceType { get; set; }
        public string Type { get; set; }
        public double ExpectedRefund { get; set; }
        public double ExpectedFee { get; set; }
        public string User { get; set; }
        public string TranslatedCaseowner { get; set; }
        public DateTime? FeeDate { get; set; }
        public bool Signed { get; set; }
        public double ActualRefund { get; set; }
        public double ActualFee { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
