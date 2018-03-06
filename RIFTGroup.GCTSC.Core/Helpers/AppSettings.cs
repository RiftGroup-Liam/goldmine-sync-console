using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.Core
{
    public class AppSettings
    {
        NameValueCollection _appSettings;
        public AppSettings()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }
        public string APIToken
        {
            get { return _appSettings["apiToken"]; }
        }

        public string ApiURL
        {
            get { return _appSettings["apiURL"]; }
        }

        public int ThreadSleep
        {
            get { return int.Parse(_appSettings["threadSleep"]); }
        }

        public bool RunAsConsole
        {
            get { if (_appSettings["runAsConsole"] == "1") { return true; } else { return false; } ;  }
        }
    }
}
