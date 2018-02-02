using RIFTGroup.GCTSC.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business.Tests.Helpers
{
    public class LastVersionNumberHelper
    {
        public int Contact1StartVersionNumber { get; set; }
        public int Contact2StartVersionNumber { get; set; }
        public int ContsuppStartVersionsNumber { get; set; }

        public LastVersionNumberHelper()
        {
            Contact1StartVersionNumber = GetContact1StartVersionNumber();
            Contact2StartVersionNumber = GetContact2StartVersionNumber();
            ContsuppStartVersionsNumber = GetContSuppStartVersionNumber();
        }

        private int GetContact1StartVersionNumber()
        {
            int contact1StartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contact1StartVersionNumber = int.Parse((from c in context.CONTACT1ChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch (Exception) { return contact1StartVersionNumber; }
            return contact1StartVersionNumber;
        }
        private int GetContSuppStartVersionNumber()
        {
            int contSuppStartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contSuppStartVersionNumber = int.Parse((from c in context.CONTSUPPChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch (Exception) { return contSuppStartVersionNumber; }
            return contSuppStartVersionNumber;
        }
        private int GetContact2StartVersionNumber()
        {
            int contact2StartVersionNumber = 1;
            try
            {
                using (GoldmineEntities context = new GoldmineEntities())
                {
                    contact2StartVersionNumber = int.Parse((from c in context.CONTACT2ChangeTracking(1) select c.SYS_CHANGE_VERSION).ToList().OrderByDescending(c => c.Value).First().Value.ToString());
                }
            }
            catch (Exception) { return contact2StartVersionNumber; }
            return contact2StartVersionNumber;
        }
    }
}
