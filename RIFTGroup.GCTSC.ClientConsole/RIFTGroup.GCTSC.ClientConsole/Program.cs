using RIFTGroup.GCTSC.Business;
using RIFTGroup.GCTSC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            VersionNumberProcess vnp = new VersionNumberProcess();
            ChangeTrackingProcess changeTrackingProcess = new ChangeTrackingProcess();
            ResultsProcessor resultsProcessor = new ResultsProcessor();
            ApplicationLogging applicationLogging = new ApplicationLogging();
            ExceptionLogging exceptionLogging = new ExceptionLogging();
            AppSettings appSettings = new AppSettings();

            do
            {
                List<CONTACT1ChangeTracking_Result> contact1Results = new List<CONTACT1ChangeTracking_Result>();
                List<CONTACT2ChangeTracking_Result> contact2Results = new List<CONTACT2ChangeTracking_Result>();
                List<CONTSUPPChangeTracking_Result> contsuppResults = new List<CONTSUPPChangeTracking_Result>();
                try
                {
                    Console.Clear();
                    Console.WriteLine("Getting CONTACT1 changes");
                    contact1Results = changeTrackingProcess.GetContact1ChangeTrackingResults(vnp.GetCurrentContact1Version());
                    Console.WriteLine("CONTACT1 changes: {0}", contact1Results.Count);

                    Console.WriteLine("Getting CONTACT2 changes");
                    contact2Results = changeTrackingProcess.GetContact2ChangeTrackingResults(vnp.GetCurrentContact2Version());
                    Console.WriteLine("CONTACT2 changes: {0}", contact2Results.Count);

                    Console.WriteLine("Getting CONTSUPP changes");
                    contsuppResults = changeTrackingProcess.GetContSuppChangeTrackingResults(vnp.GetCurrentContsuppVersion());
                    Console.WriteLine("CONTSUPP changes: {0}", contsuppResults.Count);
                }
                catch (Exception dataRetrieveError) { exceptionLogging.LogException(dataRetrieveError); }

                try
                {
                    List<ResultsObject> results = resultsProcessor.ProcessAPICalls(contact1Results, contact2Results, contsuppResults);
                    Console.WriteLine("Finished Processing: results count.... {0}", results.Count);
                }
                catch (Exception processError) { exceptionLogging.LogException(processError); }
                Thread.Sleep(appSettings.ThreadSleep);
            } while (true);
        }
    }
}
