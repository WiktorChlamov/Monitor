using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Monitor.Validation;

namespace Monitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!InputValidating(args, out Exception ex))
            {
                Console.WriteLine(ex.Message);
            }

            else
            {
                string name = args[0];
                int lifetime = int.Parse(args[1]);
                int delayChecking = int.Parse(args[2]);
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                Task task = new Task(() => KillProcess(token, name,lifetime, delayChecking));
                task.Start();

                PressingExit();

                cts.Cancel();

                task.Wait();

                ExitApplication();
            }
        }

        static void KillProcess(CancellationToken token, string name, int lifetime, int delay)
        {
            KillingProcesses killProcess = new KillingProcesses(delay,lifetime,token);
            FindingProcesses search = new FindingProcesses(name, token);
            DataTransfer dataTransfer = new DataTransfer(killProcess, search);

            Task task = new Task(() => search.StartSearch());
            task.Start();
            Task.Run(()=> killProcess.KillExpiredProcesses());
            task.Wait();
        }

        static void PressingExit()
        {
            Console.Write("Press <Q> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Q) {}
        }

        private static void ExitApplication()
        {
            Process.GetCurrentProcess().Close();
        }

        private static bool InputValidating(string[] args, out Exception ex)
        {
            ValidationChain chain = new ValidationChain();
            var result = chain.StartValidation(args);
            
            if (result != null)
            {
                ex = new Exception(result.ToString());
                return false;    
            }
            else
            {
                ex = null;
                return true;
            }
        }
    }
}
