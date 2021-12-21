using System;
using System.Threading;

namespace Parallel4
{
    public class Smoker
    {
        private Semaphore _partSemaphore;
        private string _partName;
        private Semaphore _tableSemaphore;
        public Smoker(ref Semaphore partSemaphore, ref Semaphore tableSemaphore, string partName)
        {
            _partSemaphore = partSemaphore;
            _partName = partName;
            _tableSemaphore = tableSemaphore;
        }

        public void SmokerMain()
        {
            Console.WriteLine(_partName + " smoker started smoking");
            _tableSemaphore.Release();
            Thread.Sleep(5000);
            Console.WriteLine(_partName + " smoker stopped smoking");
            _partSemaphore.Release();
        }
    }
}