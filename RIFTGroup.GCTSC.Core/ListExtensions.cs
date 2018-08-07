using RIFTGroup.GCTSC.Core.ReponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public static class ListExtensions
    {
        public static bool HasActiveAddresses<T>(this List<T> list) where T : PersonAddress
        {
            bool hasActiveAddresses = false;
            foreach(PersonAddress addressResponse in list)
            {
                if (addressResponse.Active) { hasActiveAddresses = true; goto Finish; }
            }
            Finish:
            return hasActiveAddresses;
        }
    }
}
