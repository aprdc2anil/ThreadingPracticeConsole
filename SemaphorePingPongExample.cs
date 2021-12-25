using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    public class SemaphorePingPongExample
    {
        // SemaphoreSlim is sufficient for using with in the process

        // Mutex mutex = new Mutex();
        Semaphore pingSem = new Semaphore(0, 1);
        Semaphore pongSem = new Semaphore(0, 1);

        // bool isPing = true;

        // mutex needs to be used with try catch finally 
        void Ping()
        {
            while (true)
            {
                pongSem.WaitOne();
                Console.WriteLine("Ping");
                Thread.Sleep(500);
                pingSem.Release();
            }
        }

        void Pong()
        {
            while (true)
            {
                pongSem.Release();
                pingSem.WaitOne();
                Console.WriteLine("Pong");
                Thread.Sleep(500);
            }
        }

        public void RunTest()
        {

            Thread pingThread = new Thread(() =>
            {
                Ping();
            });


            Thread pongThread = new Thread(() =>
            {
                Pong();
            });


            // when the main thread exits the child threads also termintates if they are background threads
            pingThread.IsBackground = true;
            pongThread.IsBackground = true;

            pingThread.Start();
            pongThread.Start();

            Thread.Sleep(10000);
        }
    }
}

