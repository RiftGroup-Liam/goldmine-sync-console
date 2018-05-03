using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.Tests.Helpers
{
    public class ClientDataHelper
    {
        public ClientData ClientData
        {
            get
            {
                return new ClientData()
                {
                    Key5 = "165992",
                    Secr = "Client",
                    Lastname = "Data",
                    Phone1 = "07887495880",
                    Phone2 = "07884958800",
                    Phone3 = "07849588000",
                    UconvDate = "2018-02-13",
                    UStage1Dat = "2018-02-13",
                    UEmailAddr = "larnold@riftgroup.com",
                    EmailPreference = "Yes",
                    PhonePreference = "No",
                    SMSPreference = "Yes",
                    PostPreference = "No",
                };
            }
        }
    }
}
