using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business
{
    public class ExceptionLogging
    {
        public void LogException(Exception givenException)
        {
            GCTSC_ExceptionLogging exception = new GCTSC_ExceptionLogging()
            {
                Message = givenException.Message,
                StackTrace = givenException.StackTrace,
                Created = DateTime.Now
            };
            if(givenException.InnerException!=null)
            {
                exception.InnerMessage = givenException.InnerException.Message;
            }
            SaveException(exception);
        }

        private void SaveException(GCTSC_ExceptionLogging exception)
        {
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.GCTSC_ExceptionLogging.Add(exception);
                    context.SaveChanges();
                }
                catch (Exception ex) { throw ex; }
            }
        }
    }
}
