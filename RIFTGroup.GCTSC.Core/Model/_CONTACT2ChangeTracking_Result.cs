using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public partial class CONTACT2ChangeTracking_Result
    {
        public bool uemailaddrChanged_bool { get { return uemailaddrChanged.Value == 1; } }
        public bool ustage1dat_bool { get { return ustage1datChanged.Value == 1; } }
        public bool uconvdate_bool { get { return uconvdatChanged.Value == 1; } }
        public bool ubcaseown_bool { get { return ubcaseownChanged.Value == 1; } }
        public bool ucpemail_bool { get { return ucpemailChanged.Value == 1; } }
        public bool ucpphone_bool { get { return ucpphoneChanged.Value == 1; } }
        public bool ucpsms_bool { get { return ucpsmsChanged.Value == 1; } }
        public bool ucppost_bool { get { return ucppostChanged.Value == 1; } }
    }
}
