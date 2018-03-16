using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core.ReponseObjects
{
    public class CreateCaseOwnerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; } 
        public DateTime Updated_at { get; set; }
    }
}
