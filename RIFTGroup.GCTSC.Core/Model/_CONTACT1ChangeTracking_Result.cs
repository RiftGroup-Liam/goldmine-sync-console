using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public partial class CONTACT1ChangeTracking_Result
    {
        public bool key5Changed_bool
        {
            get
            { return key5Changed.Value == 1; }
        }

        public bool contactChanged_bool
        {
            get
            {
                return contactChanged.Value == 1;
            }
        }

        public bool secrChanged_bool
        {
            get
            {
                return secrChanged.Value == 1;
            }
        }

        public bool LastnameChanged_bool
        {
            get
            {
                return LastnameChanged.Value == 1;
            }
        }

        public bool phone1Changed_bool
        {
            get
            {
                return phone1Changed.Value == 1;
            }
        }

        public bool phone2Changed_bool
        {
            get
            {
                return phone2Changed.Value == 1;
            }
        }

        public bool phone3Changed_bool
        {
            get
            {
                return phone3Changed.Value == 1;
            }
        }
    }
}
