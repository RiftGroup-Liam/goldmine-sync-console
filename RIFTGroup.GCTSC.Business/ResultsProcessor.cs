using RIFTGroup.GCTSC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business
{
    public class ResultsProcessor
    {
        RequestProcessor _requestProcessor;
        GM_Repository _gmRepo;
        public ResultsProcessor()
        {
            _requestProcessor = new RequestProcessor();
            _gmRepo = new GM_Repository();
        }
        public List<ResultsObject> ProcessAPICalls(List<CONTACT1ChangeTracking_Result> contact1Results,
                                                    List<CONTACT2ChangeTracking_Result> contact2Results,
                                                    List<CONTSUPPChangeTracking_Result> contsuppResults)
        {
            Console.Clear();
            Console.WriteLine("Creating API Requests");
            List<ResultsObject> results = new List<ResultsObject>();

            Console.WriteLine("Sending CONTACT 1 API Requests");
            List<ResultsObject> contact1SendResults = SendContact1Changes(contact1Results);
            Console.WriteLine("Sending CONTACT 2 API Requests");
            List<ResultsObject> contact2SendResults = SendContact2Changes(contact2Results);
            Console.WriteLine("Sending CONTSUPP API Requests");
            List<ResultsObject> contsuppSendResults = SendContsuppChanges(contsuppResults);
            results = AmalgamateResults(contact1SendResults, contact2SendResults, contsuppSendResults);
            return results;
        }

        private List<ResultsObject> AmalgamateResults(List<ResultsObject> contact1SendResults, List<ResultsObject> contact2SendResults, List<ResultsObject> contsuppResults)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            results.AddRange(contact1SendResults);
            results.AddRange(contact2SendResults);
            results.AddRange(contsuppResults);
            return results;
        }

        private List<ResultsObject> SendContsuppChanges(List<CONTSUPPChangeTracking_Result> contsuppResults)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTSUPPChangeTracking_Result ctResult in contsuppResults)
            {
                ResultsObject ro = new ResultsObject();
                ro = _gmRepo.GetAccountnoFromRecid(ctResult.recid, ro);
                ro = _gmRepo.GetReferenceNumberFromAccountno(ro.Accountno, ro);
                if (!string.IsNullOrEmpty(_requestProcessor.ProcessPersonIdFetch(ro.ReferenceNumber)))
                {
                    ro = _requestProcessor.ProcessContsuppRequests(ctResult);                    
                }
                else
                {
                    ClientData clientData = new ClientData();
                    clientData = _gmRepo.GetClientData(ro.ReferenceNumber);
                    ro = _requestProcessor.ProcessCreatePersonRequest(clientData);
                }
                results.Add(ro);
            }
            return results;
        }

        private List<ResultsObject> SendContact2Changes(List<CONTACT2ChangeTracking_Result> contact2Results)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTACT2ChangeTracking_Result ctResult in contact2Results)
            {
                ResultsObject ro = new ResultsObject();
                ro.Accountno = ctResult.ACCOUNTNO;
                ro = _gmRepo.GetReferenceNumberFromAccountno(ro.Accountno, ro);
                if (!string.IsNullOrEmpty(_requestProcessor.ProcessPersonIdFetch(ro.ReferenceNumber)))
                {
                    ro = _requestProcessor.ProcessContact2Requests(ctResult);
                }
                else
                {
                    ClientData clientData = new ClientData();
                    clientData = _gmRepo.GetClientData(ro.ReferenceNumber);
                    ro = _requestProcessor.ProcessCreatePersonRequest(clientData);
                }
                results.Add(ro);
            }
            return results;
        }

        private List<ResultsObject> SendContact1Changes(List<CONTACT1ChangeTracking_Result> contact1Results)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTACT1ChangeTracking_Result ctResult in contact1Results)
            {
                ResultsObject ro = new ResultsObject();
                ro.Accountno = ctResult.ACCOUNTNO;
                ro = _gmRepo.GetReferenceNumberFromAccountno(ro.Accountno, ro);
                if (!string.IsNullOrEmpty(_requestProcessor.ProcessPersonIdFetch(ro.ReferenceNumber)))
                {
                    ro = _requestProcessor.ProcessContact1Requests(ctResult);
                }
                else
                {
                    ClientData clientData = new ClientData();
                    clientData = _gmRepo.GetClientData(ro.ReferenceNumber);
                    ro = _requestProcessor.ProcessCreatePersonRequest(clientData);
                }
                results.Add(ro);
            }
            return results;
        }
    }
}
