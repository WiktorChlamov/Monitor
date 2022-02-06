using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Monitor
{
    internal class KillingProcesses
    {   
        private int _delayToCheck;
        private int _appTimeToLife;
        private Dictionary<int, Process> _processes = new Dictionary<int, Process>();
        private CancellationToken _token;

        public KillingProcesses(int timeToCheck, int timeToLife, CancellationToken token)
        {
            _delayToCheck = timeToCheck;
            _appTimeToLife = timeToLife;
            _token = token;
        }

        public void AddProcesses(Process process)
        {
                if (!_processes.ContainsKey(process.Id))
                { 
                    _processes.Add(process.Id, process); 
                }
        }

        public void KillExpiredProcesses()
        {
            while(!_token.IsCancellationRequested)
            {
                try
                {
                    CheckValidTime();                   
                }

                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }

                finally
                {

                    Thread.Sleep(_delayToCheck * 60000);
                    
                    if (_processes.Count > 0)
                    {
                        foreach (int id in _processes.Keys.ToArray())
                        {
                            if (!_processes[id].HasExited)
                            {
                                if ((DateTime.Now - _processes[id].StartTime).TotalMinutes > _appTimeToLife)
                                {
                                    Console.WriteLine($"Приложение { _processes[id].ProcessName} закрыто"); ;
                                    _processes[id].Kill();
                                    _processes.Remove(id);
                                }
                            }
                            else
                            {
                                //Console.WriteLine($"Приложение { _processes[id].ProcessName} закрыто вручную"); ;
                                _processes.Remove(id);
                            }
                        }
                    }
                }
            }
        }

        private void CheckValidTime()
        {
            if (_delayToCheck < 0)
            {
                throw new Exception("Интервал не может быть меньше 0");
            }
        }
    }
}
