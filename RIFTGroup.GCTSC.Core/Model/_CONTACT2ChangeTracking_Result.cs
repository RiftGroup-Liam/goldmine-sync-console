using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public partial class CONTACT2ChangeTracking_Result
    {
        public bool uemailaddrChanged_bool
        {
            get
            {
                return uemailaddrChanged.Value == 1;
            }
        }

        public bool uclientstaChanged_bool { get { return uclientstaChanged.Value == 1; } }
    }
}
