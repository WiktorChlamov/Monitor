using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor
{
    internal class FindingProcesses
    {
        string _name;
        int _monitoringTime = 1;
        
        Process[] _processes;

        private CancellationToken _token;

        public delegate void NewData(Process[] processes);
        public event NewData Notify;


        public FindingProcesses(string name, CancellationToken token)
        {
            _token = token;
            _name = name;
        }

        private void FindProcess()
        {
            Process[] localByName = Process.GetProcessesByName(_name);
            if(localByName.Length>0)
            {
                _processes = localByName;
            }
        }

        public void StartSearch()
        {
            while(!_token.IsCancellationRequested)
            { 
                Thread.Sleep(_monitoringTime*1000);
                try
                {
                    TimeCheck();
                    NameCheck();
                }

                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }

                finally
                { 
                    FindProcess();

                    if(_processes != null && _processes.Length>0)
                 {
                     Notify.Invoke(_processes);
                    _processes = null;
                     
                 }
                }
            }
        }

        private void NameCheck()
        {
            if (_name.Length == 0)
            {
                throw new Exception("Не задано имя");
            }
        }

        private void TimeCheck()
        {
            if (_monitoringTime <= 0)
            {
                throw new Exception("Слишком маленькое время для посиска");
            }
        }
    }
}
