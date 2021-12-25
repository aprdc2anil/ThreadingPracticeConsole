using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    public class MutexPingPongExample
    {
        Mutex mutex = new Mutex();
        bool isPing = true;


        // mutex needs to be used with try catch finally 
        void Ping()
        {
            while (true)
            {
                try
                {
                    mutex.WaitOne();

                    while (!isPing)
                    {
                        mutex.ReleaseMutex();
                        Thread.Sleep(500);
                        mutex.WaitOne();
                    }

                    Console.WriteLine("Ping");
                    isPing = false;
                }
                catch { 
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        void Pong()
        {
            while (true)
            {
                try
                {
                    mutex.WaitOne();

                    while (isPing)
                    {
                        mutex.ReleaseMutex();
                        Thread.Sleep(500);
                        mutex.WaitOne();
                    }

                    Console.WriteLine("Pong");
                    isPing = true;
                }
                catch (Exception e)
                {
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
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

