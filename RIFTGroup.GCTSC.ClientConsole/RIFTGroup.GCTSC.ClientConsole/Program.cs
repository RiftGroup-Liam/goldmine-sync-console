using RIFTGroup.GCTSC.Business;
using RIFTGroup.GCTSC.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RIFTGroup.GCTSC.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProcess runProcess = new RunProcess();
            runProcess.Run();
        }
    }
}
