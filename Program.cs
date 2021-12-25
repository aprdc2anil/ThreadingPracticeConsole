using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    /*
     * Mutex -- Low level construct for locking
     */ 

    class Program
    {
        static void Main(string[] args)
        {
            // new DeadlockExample().RunTest();

            // new MutexBusyWaitProducerConsumer().RunTest();

            // new MutexBusyWaitProducerConsumerSingleMutex().RunTest();

            // new ThreadPoolExample().RunTest();

            // new ThreadInterruptExample().RunTest();

            // new MutexPingPongExample().RunTest();

            // new SemaphorePingPongExample().RunTest();

            // new MonitorPingPongExample().RunTest();

            new MonitorWaitPrimeNumberExample().RunTest();

        }
    }
}
