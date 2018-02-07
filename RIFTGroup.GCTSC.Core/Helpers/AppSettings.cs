﻿using System;
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
    }
}