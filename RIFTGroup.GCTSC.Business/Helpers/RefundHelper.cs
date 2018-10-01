using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core.Model;

namespace RIFTGroup.GCTSC.Business.Helpers
{
    public class RefundHelper
    {
        public Refund CreateRefund(string servt, string type, double? expref, double? expfee, DateTime? feedat, string signed,
                double? actfee, double? actref, DateTime? completedDate, string user, int year, string accountno)
        {
            Refund refund = new Refund()
            {
                ServiceType = servt,
                Signed = signed == "YES" ? true : false,
                User = user,
                Type = type,
                Year = year
            };
            if (actfee.HasValue)
            { refund.ActualFee = actfee.Value; }
            if (actref.HasValue)
            { refund.ActualRefund = actref.Value; }
            if (completedDate.HasValue)
            { refund.CompletedAt = completedDate.Value; }
            else
            {
                refund.CompletedAt = null;
            }
            if (expfee.HasValue)
            { refund.ExpectedFee = expfee.Value; }
            if (expref.HasValue)
            { refund.ExpectedRefund = expref.Value; }
            if (feedat.HasValue)
            { refund.FeeDate = feedat.Value; }
            else
            {
                refund.FeeDate = null;
            }
            return refund;
        }
    }
}
