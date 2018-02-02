using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business
{
    public class ChangeTrackingProcess
    {
        VersionNumberProcess _versionNumberProcess;
        public ChangeTrackingProcess()
        {
            _versionNumberProcess = new VersionNumberProcess();
        }
        public List<CONTACT1ChangeTracking_Result> GetContact1ChangeTrackingResults(int _contact1StartVersionNumber)
        {
            List<CONTACT1ChangeTracking_Result> results = new List<CONTACT1ChangeTracking_Result>();
            using (GoldmineEntities context = new GoldmineEntities())
            {
                results = (from c in context.CONTACT1ChangeTracking(_contact1StartVersionNumber) select c).ToList();
            }
            if (results.Count > 0)
            {
                _versionNumberProcess.UpdateContact1VersionNumber(int.Parse(results.OrderByDescending(x => x.SYS_CHANGE_VERSION).FirstOrDefault().SYS_CHANGE_VERSION.Value.ToString()));
            }
            return results;
        }

        public List<CONTACT2ChangeTracking_Result> GetContact2ChangeTrackingResults(int _contact2StartVersionNumber)
        {
            List<CONTACT2ChangeTracking_Result> results = new List<CONTACT2ChangeTracking_Result>();
            using (GoldmineEntities context = new GoldmineEntities())
            {
                results = (from c in context.CONTACT2ChangeTracking(_contact2StartVersionNumber) select c).ToList();
            }
            if (results.Count > 0)
            {
                _versionNumberProcess.UpdateContact2VersionNumber(int.Parse(results.OrderByDescending(x => x.SYS_CHANGE_VERSION).FirstOrDefault().SYS_CHANGE_VERSION.Value.ToString()));
            }
            return results;
        }

        public List<CONTSUPPChangeTracking_Result> GetContSuppChangeTrackingResults(int _contsuppStartVersionNumber)
        {
            List<CONTSUPPChangeTracking_Result> results = new List<CONTSUPPChangeTracking_Result>();
            using (GoldmineEntities context = new GoldmineEntities())
            {
                results = (from c in context.CONTSUPPChangeTracking(_contsuppStartVersionNumber) select c).ToList();
            }
            if (results.Count > 0)
            {
                _versionNumberProcess.UpdateContsuppVersionNumber(int.Parse(results.OrderByDescending(x => x.SYS_CHANGE_VERSION).FirstOrDefault().SYS_CHANGE_VERSION.Value.ToString()));
            }
            return results;
        }
    }
}
