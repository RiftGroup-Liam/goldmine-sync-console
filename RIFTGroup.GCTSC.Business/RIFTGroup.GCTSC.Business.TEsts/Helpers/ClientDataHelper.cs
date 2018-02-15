using RIFTGroup.GCTSC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Business.Tests.Helpers
{
    public class ClientDataHelper
    {
        public ClientData TestData
        {
            get
            {
                return new ClientData()
                {
                    Accountno = "B5040182438 F[6CPLia",
                    Key5 = CreateRandomReference(),
                    Lastname = "Arnold",
                    Phone1 = "07887495880",
                    Phone2 = "01304362644",
                    Phone3 = "01227453980",
                    Secr = "Liam",
                    UconvDate = "14/02/2018 00:00:00",
                    UStage1Dat = "13/02/2018 00:00:00",
                    UEmailAddr = "liamarnold93@gmail.com"
                };
            }
        }

        private string CreateRandomReference()
        {
            Random rnd = new Random();
            return rnd.Next(1, 333333).ToString(); ;
        }
    }
}
