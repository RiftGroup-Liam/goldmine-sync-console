using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business
{
    public class VersionNumberProcess
    {
        private const string Contact1_LastVersion = "Contact1_LastVersion";
        private const string Contact2_LastVersion = "Contact2_LastVersion";
        private const string Contsupp_LastVersion = "ContSupp_LastVersion";

        public int GetCurrentContact1Version()
        {
            int currentVersion = 1;
            using (RiftEntities context = new RiftEntities())
            {
                string result = (from sk in context.SystemKeys.Where(sk => sk.Key == Contact1_LastVersion) select sk.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    currentVersion = int.Parse(result);
                }
            }
            return currentVersion;
        }

        public int GetCurrentContact2Version()
        {
            int currentVersion = 1;
            using (RiftEntities context = new RiftEntities())
            {
                string result = (from sk in context.SystemKeys.Where(sk => sk.Key == Contact2_LastVersion) select sk.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    currentVersion = int.Parse(result);
                }
            }
            return currentVersion;
        }

        public int GetCurrentContsuppVersion()
        {
            int currentVersion = 1;
            using (RiftEntities context = new RiftEntities())
            {
                string result = (from sk in context.SystemKeys.Where(sk => sk.Key == Contsupp_LastVersion) select sk.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(result))
                {
                    currentVersion = int.Parse(result);
                }
            }
            return currentVersion;
        }

        public void UpdateContact1VersionNumber(int newVersion)
        {
            using (RiftEntities context = new RiftEntities())
            {
                SystemKey systemKey = (from sk in context.SystemKeys.Where(sk => sk.Key == Contact1_LastVersion) select sk).FirstOrDefault();
                if(systemKey != null)
                {
                    systemKey.Value = newVersion.ToString();
                    context.Entry(systemKey).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateContact2VersionNumber(int newVersion)
        {
            using (RiftEntities context = new RiftEntities())
            {
                SystemKey systemKey = (from sk in context.SystemKeys.Where(sk => sk.Key == Contact2_LastVersion) select sk).FirstOrDefault();
                if (systemKey != null)
                {
                    systemKey.Value = newVersion.ToString();
                    context.Entry(systemKey).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateContsuppVersionNumber(int newVersion)
        {
            using (RiftEntities context = new RiftEntities())
            {
                SystemKey systemKey = (from sk in context.SystemKeys.Where(sk => sk.Key == Contsupp_LastVersion) select sk).FirstOrDefault();
                if (systemKey != null)
                {
                    systemKey.Value = newVersion.ToString();
                    context.Entry(systemKey).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
    }
}
