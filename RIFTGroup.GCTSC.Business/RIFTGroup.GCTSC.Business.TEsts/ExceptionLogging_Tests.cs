using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RIFTGroup.GCTSC.Business;
using RIFTGroup.GCTSC.Core.EntityFramework;
using System.Linq;
using RIFTGroup.GCTSC.Core;

namespace RIFTGroup.GCTSC.Business.Tests
{
    [TestClass]
    public class ExceptionLogging_Tests
    {
        ExceptionLogging _exceptionLogging;
        public ExceptionLogging_Tests()
        {
            _exceptionLogging = new ExceptionLogging();
        }
        [TestInitialize]
        public void DoInit()
        {
            ClearExceptionTable();
        }

        private void ClearExceptionTable()
        {
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.TESTs_ClearGCTSCExceptionTable();
                }
                catch (Exception ex) { throw ex; }
            }
        }

        [TestMethod]
        public void SuccessfullyLogsException()
        {
            Exception pretendException = new Exception("Lets see if we can log an exception", new Exception("with an inner exception"));
            _exceptionLogging.LogException(pretendException);
            CheckExceptionExists();
        }

        private void CheckExceptionExists()
        {
            using (RiftEntities context = new RiftEntities())
            {
                GCTSC_ExceptionLogging exception = (from ex in context.GCTSC_ExceptionLogging.Where(ex => ex.Message.Contains("Lets see if we") &&
                                                                        ex.InnerMessage.Contains("with an inner"))
                 select ex).FirstOrDefault();
                Assert.IsNotNull(exception);
            }
        }
    }
}
