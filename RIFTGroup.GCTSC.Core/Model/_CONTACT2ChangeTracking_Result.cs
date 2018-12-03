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

        #region service_type
        public bool u14servt_bool { get { return U14SERVT.Value == 1; } }
        public bool u15servt_bool { get { return U15SERVT.Value == 1; } }
        public bool u16servt_bool { get { return U16SERVT.Value == 1; } }
        public bool u17servt_bool { get { return U17SERVT.Value == 1; } }
        public bool u18servt_bool { get { return U18SERVT.Value == 1; } }
        public bool u19servt_bool { get { return U19SERVT.Value == 1; } }
        public bool u20servt_bool { get { return U20SERVT.Value == 1; } }
        #endregion

        #region type
        public bool uy14type_bool { get { return UY14TYPE.Value == 1; } }
        public bool uy15type_bool { get { return UY15TYPE.Value == 1; } }
        public bool u16type_bool { get { return U16TYPE.Value == 1; } }
        public bool u17type_bool { get { return U17TYPE.Value == 1; } }
        public bool u18type_bool { get { return U18TYPE.Value == 1; } }
        public bool u19type_bool { get { return U19TYPE.Value == 1; } }
        public bool u20type_bool { get { return U20TYPE.Value == 1; } }
        #endregion

        #region expected_refund
        public bool uy14expref_bool { get { return UY14EXPREF.Value == 1; } }
        public bool uy15expref_bool { get { return UY15EXPREF.Value == 1; } }
        public bool u16expref_bool { get { return U16EXPREF.Value == 1; } }
        public bool u17expref_bool { get { return U17EXPREF.Value == 1; } }
        public bool u18expref_bool { get { return U18EXPREF.Value == 1; } }
        public bool u19expref_bool { get { return U19EXPREF.Value == 1; } }
        public bool u20expref_bool { get { return U20EXPREF.Value == 1; } }
        #endregion

        #region expected_fee
        public bool uy14expfee_bool { get { return UY14EXPFEE.Value == 1; } }
        public bool uy15expfee_bool { get { return UY15EXPFEE.Value == 1; } }
        public bool u16expfee_bool { get { return U16EXPFEE.Value == 1; } }
        public bool u17expfee_bool { get { return U17EXPFEE.Value == 1; } }
        public bool u18expfee_bool { get { return U18EXPFEE.Value == 1; } }
        public bool u19expfee_bool { get { return U19EXPFEE.Value == 1; } }
        public bool u20expfee_bool { get { return U20EXPFEE.Value == 1; } }
        #endregion

        #region user
        public bool uy14user_bool { get { return UY14USER.Value == 1; } }
        public bool uy15user_bool { get { return UY15USER.Value == 1; } }
        public bool u16user_bool { get { return U16USER.Value == 1; } }
        public bool u17user_bool { get { return U17USER.Value == 1; } }
        public bool u18user_bool { get { return U18USER.Value == 1; } }
        public bool u19user_bool { get { return U19USER.Value == 1; } }
        public bool u20user_bool { get { return U20USER.Value == 1; } }
        #endregion

        #region fee_date
        public bool uy14feedat_bool { get { return UY14FEEDAT.Value == 1; } }
        public bool uy15feedat_bool { get { return UY15FEEDAT.Value == 1; } }
        public bool u16feedat_bool { get { return U16FEEDAT.Value == 1; } }
        public bool u17feedat_bool { get { return U17FEEDAT.Value == 1; } }
        public bool u18feedat_bool { get { return U18FEEDAT.Value == 1; } }
        public bool u19feedat_bool { get { return U19FEEDAT.Value == 1; } }
        public bool u20feedat_bool { get { return U20FEEDAT.Value == 1; } }
        #endregion

        #region signed
        public bool uy14signed_bool { get { return UY14SIGNED.Value == 1; } }
        public bool uy15signed_bool { get { return UY15SIGNED.Value == 1; } }
        public bool u16signed_bool { get { return U16SIGNED.Value == 1; } }
        public bool u17signed_bool { get { return U17SIGNED.Value == 1; } }
        public bool u18signed_bool { get { return U18SIGNED.Value == 1; } }
        public bool u19signed_bool { get { return U19SIGNED.Value == 1; } }
        public bool u20signed_bool { get { return U20SIGNED.Value == 1; } }
        #endregion

        #region actual_fee
        public bool uy14actfee_bool { get { return UY14ACTFEE.Value == 1; } }
        public bool uy15actfee_bool { get { return UY15ACTFEE.Value == 1; } }
        public bool u16actfee_bool { get { return U16ACTFEE.Value == 1; } }
        public bool u17actfee_bool { get { return U17ACTFEE.Value == 1; } }
        public bool u18actfee_bool { get { return U18ACTFEE.Value == 1; } }
        public bool u19actfee_bool { get { return U19ACTFEE.Value == 1; } }
        public bool u20actfee_bool { get { return U20ACTFEE.Value == 1; } }
        #endregion

        #region actual_refund
        public bool uy14actref_bool { get { return UY14ACTREF.Value == 1; } }
        public bool uy15actref_bool { get { return UY15ACTREF.Value == 1; } }
        public bool u16actref_bool { get { return U16ACTREF.Value == 1; } }
        public bool u17actref_bool { get { return U17ACTREF.Value == 1; } }
        public bool u18actref_bool { get { return U18ACTREF.Value == 1; } }
        public bool u19actref_bool { get { return U19ACTREF.Value == 1; } }
        public bool u20actref_bool { get { return U20ACTREF.Value == 1; } }
        #endregion

        public bool userdef01_bool { get { return USERDEF01.Value == 1; } }
        public bool userdef02_bool { get { return USERDEF02.Value == 1; } }

        public bool unsource_bool {  get { return UNSOURCE.Value == 1; } }
    }
}
