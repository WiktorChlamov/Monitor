using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Monitor
{
    internal class DataTransfer
    {
       private KillingProcesses _killingProcesses;
       private FindingProcesses _findingProcesses;

        public DataTransfer(KillingProcesses killingProcesses, FindingProcesses findingProcesses)
        {
            this._killingProcesses = killingProcesses;
            _findingProcesses = findingProcesses;
            _findingProcesses.Notify += Transfering;
        }

        private void Transfering(Process[] processes)
        {
            foreach (Process process in processes)
            {
                _killingProcesses.AddProcesses(process);
            }
        }
    }
}
