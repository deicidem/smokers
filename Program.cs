using System;
using System.Threading;

namespace Parallel4
{
    class Program
    {
        static void Main(string[] args)
        {
            var tabakSemaphore = new Semaphore(1, 1);
            var paperSemaphore = new Semaphore(1, 1);
            var fireSemaphore = new Semaphore(1, 1);
            var tableSemaphore = new Semaphore(1, 1);

            var tabakSmoker = new Smoker(ref tabakSemaphore, ref tableSemaphore, "tabak");
            var paperSmoker = new Smoker(ref paperSemaphore, ref tableSemaphore, "paper");
            var fireSmoker = new Smoker(ref fireSemaphore, ref tableSemaphore, "fire");


            void HelperMain()
            {
                while (true)
                {
                    tableSemaphore.WaitOne();
                    var random = new Random();
                    var part = random.Next(1, 4);
                    Console.WriteLine(part);
                    switch (part)
                    {
                        case 1:
                            Console.WriteLine("paper and fire are on the table");
                            tabakSemaphore.WaitOne();
                            var tabakSmokerThread = new Thread(tabakSmoker.SmokerMain);
                            tabakSmokerThread.Start();
                            break;
                        case 2:
                            Console.WriteLine("tabak and fire are on the table");
                            paperSemaphore.WaitOne();
                            var paperSmokerThread = new Thread(paperSmoker.SmokerMain);
                            paperSmokerThread.Start();
                            break;
                        case 3:
                            Console.WriteLine("tabak and paper are on the table");
                            fireSemaphore.WaitOne();
                            var fireSmokerThread = new Thread(fireSmoker.SmokerMain);
                            fireSmokerThread.Start();
                            break;
                    }
                }
            }
            

            var helperThread = new Thread(HelperMain);
            helperThread.Start();
        }
    }
}