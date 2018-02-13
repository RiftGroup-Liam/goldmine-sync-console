using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIFTGroup.GCTSC.Core;
using RIFTGroup.GCTSC.Core.EntityFramework;

namespace RIFTGroup.GCTSC.Business
{
    public class ApplicationLogging
    {
        public void Log(List<ResultsObject> results)
        {
            foreach(ResultsObject ro in results)
            {
                Log(ro);
            }
        }

        public void Log(ResultsObject resultObject)
        {
            GCTSC_ChangeTracking ctRecord = new GCTSC_ChangeTracking()
            {
                Accountno = resultObject.Accountno,
                Created = DateTime.Now
            };
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.GCTSC_ChangeTracking.Add(ctRecord);
                    context.SaveChanges();
                }
                catch(Exception ex) { throw ex; }
            }

            if(ctRecord.Id > 0 && resultObject.Responses.Count > 0)
            {
                SaveResponseInformation(ctRecord, resultObject.Responses);
            }
        }

        private void SaveResponseInformation(GCTSC_ChangeTracking ctRecord, List<ResponseDetails> responses)
        {
            foreach(ResponseDetails response in responses)
            {
                if(response.SendRequest == 0)
                {
                    SaveGeneralRequestInformation(response, ctRecord);
                }
                else
                {
                    SaveUpdateRequestInformation(response, ctRecord);
                }
            }
        }

        private void SaveGeneralRequestInformation(ResponseDetails response, GCTSC_ChangeTracking ctRecord)
        {
            GCTSC_ChangeTracking_Requests request = new GCTSC_ChangeTracking_Requests()
            {
                ChangeTrackingId = ctRecord.Id,
                ResponseCode = response.SendResponse.ToString(),
                ResponseContent = response.ResponseContent,
                URL = response.URL
            };
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.GCTSC_ChangeTracking_Requests.Add(request);
                    context.SaveChanges();
                }
                catch (Exception ex) { throw ex; }
            }
        }

        private void SaveUpdateRequestInformation(ResponseDetails response, GCTSC_ChangeTracking ctRecord)
        {
            GCTSC_ChangeTracking_UpdateRequests updateRequest = new GCTSC_ChangeTracking_UpdateRequests()
            {
                ChangeTrackingId = ctRecord.Id,
                Change = response.SendRequest.ToString(),
                ResponseCode = response.SendResponse.ToString(),
                ResponseContent = response.ResponseContent,
                URL = response.URL
            };
            using (RiftEntities context = new RiftEntities())
            {
                try
                {
                    context.GCTSC_ChangeTracking_UpdateRequests.Add(updateRequest);
                    context.SaveChanges();
                }
                catch (Exception ex) { throw ex; }
            }
        }
    }
}
