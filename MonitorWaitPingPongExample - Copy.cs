using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    public class MonitorWaitPingPongExample
    { 
        bool isPing = true;
        object lockObject = new object();

        // mutex needs to be used with try catch finally 
        void Ping()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(lockObject);

                    while (!isPing)
                    {
                        Monitor.Wait(lockObject);
                    }

                    Console.WriteLine("Ping");
                    isPing = false;
                    Monitor.Pulse(lockObject);
                }
                catch { 
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }
            }
        }

        void Pong()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(lockObject);

                    while (isPing)
                    {
                        Monitor.Wait(lockObject);
                    }

                    Console.WriteLine("Pong");
                    isPing = true;
                    Monitor.Pulse(lockObject);
                }
                catch (Exception e)
                {
                }
                finally
                {
                    Monitor.Exit(lockObject);
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

