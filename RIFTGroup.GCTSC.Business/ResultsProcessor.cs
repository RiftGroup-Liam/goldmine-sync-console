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
        public ResultsProcessor()
        {
            _requestProcessor = new RequestProcessor();
        }
        public List<ResultsObject> ProcessAPICalls(List<CONTACT1ChangeTracking_Result> contact1Results,
                                                    List<CONTACT2ChangeTracking_Result> contact2Results,
                                                    List<CONTSUPPChangeTracking_Result> contsuppResults)
        {
            List<ResultsObject> results = new List<ResultsObject>();

            List<ResultsObject> contact1SendResults = SendContact1Changes(contact1Results);
            List<ResultsObject> contact2SendResults = SendContact2Changes(contact2Results);
            List<ResultsObject> contsuppSendResults = SendContsuppChanges(contsuppResults);
            return results;
        }

        private List<ResultsObject> SendContsuppChanges(List<CONTSUPPChangeTracking_Result> contsuppResults)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTSUPPChangeTracking_Result ctResult in contsuppResults)
            {
                ResultsObject ro = _requestProcessor.ProcessContsuppRequests(ctResult);
            }
            return results;
        }

        private List<ResultsObject> SendContact2Changes(List<CONTACT2ChangeTracking_Result> contact2Results)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTACT2ChangeTracking_Result ctResult in contact2Results)
            {
                ResultsObject ro = _requestProcessor.ProcessContact2Requests(ctResult);
            }
            return results;
        }

        private List<ResultsObject> SendContact1Changes(List<CONTACT1ChangeTracking_Result> contact1Results)
        {
            List<ResultsObject> results = new List<ResultsObject>();
            foreach(CONTACT1ChangeTracking_Result ctResult in contact1Results)
            {
                ResultsObject ro = _requestProcessor.ProcessContact1Requests(ctResult);
            }
            return results;
        }
    }
}
