using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    public class MonitorWaitPrimeNumberExample
    { 
        bool isPrimeFound = true;
        int primeNumber = 2;
        object lockObject = new object();

        // mutex needs to be used with try catch finally 
        void FindPrime()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(lockObject);

                    while (isPrimeFound)
                    {
                        Monitor.Wait(lockObject);
                    }

                    ++primeNumber;

                    while (!IsPrime(primeNumber))
                    {
                        ++primeNumber;
                    }

                    isPrimeFound = true;

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

        bool IsPrime(int number)
        {
            bool isPrime = true;
            for(int i=2; i<=(int)Math.Sqrt(number);++i)
            {
                if (number%i==0)
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }

        void PrintPrime()
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(lockObject);

                    while (!isPrimeFound)
                    {
                        Monitor.Wait(lockObject);
                    }

                    Console.WriteLine("Prime Number: {0}", primeNumber);
                    Thread.Sleep(500);
                    isPrimeFound = false;
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
                FindPrime();
            });


            Thread pongThread = new Thread(() =>
            {
                PrintPrime();
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

