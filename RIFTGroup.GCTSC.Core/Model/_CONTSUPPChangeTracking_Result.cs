using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public partial class CONTSUPPChangeTracking_Result
    {
        public bool contsuprefChanged_bool { get { return contsuprefChanged.Value == 1; } }
    }
}
