using RIFTGroup.GCTSC.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        RunProcess _runProcess = new RunProcess();
        Thread _thread;
        public Service1()
        {
            InitializeComponent();
            _thread = new Thread(new ThreadStart(_runProcess.Run));
        }

        protected override void OnStart(string[] args)
        {
            _thread.Start();
        }

        protected override void OnStop()
        {
            _thread.Abort();
            Environment.Exit(0);
        }
    }
}
