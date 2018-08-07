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
        public bool udob_bool { get { return UDOB.Value == 1; } }

        #region Completed_Dates
        // Completed Dates
        public bool uy12com_bool { get { return UY12COM.Value == 1; } }
        public bool uy13com_bool { get { return UY13COM.Value == 1; } }
        public bool uy14com_bool { get { return UY14COM.Value == 1; } }
        public bool uy15com_bool { get { return UY15COM.Value == 1; } }
        public bool uy16com_bool { get { return UY16COM.Value == 1; } }
        public bool uy17com_bool { get { return UY17COM.Value == 1; } }
        public bool uy18com_bool { get { return UY18COM.Value == 1; } }
        public bool uy19com_bool { get { return UY19COM.Value == 1; } }
        public bool uy20com_bool { get { return UY20COM.Value == 1; } }
        public bool uy21com_bool { get { return UY21COM.Value == 1; } }
        public bool uy22com_bool { get { return UY22COM.Value == 1; } }
        #endregion
    }
}
