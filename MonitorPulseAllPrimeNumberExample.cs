using System;
using System.Threading;

namespace ThreadingPracticeConsole
{
    public class MonitorPulseAllPrimeNumberExample
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

                    Monitor.PulseAll(lockObject);
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
                    Monitor.PulseAll(lockObject);
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

            Thread finder1 = new Thread(() =>
            {
                FindPrime();
            });

            Thread finder2 = new Thread(() =>
            {
                FindPrime();
            });

            Thread finder3 = new Thread(() =>
            {
                FindPrime();
            });


            Thread printerThread = new Thread(() =>
            {
                PrintPrime();
            });

            // when the main thread exits the child threads also termintates if they are background threads
            finder1.IsBackground = true;
            finder2.IsBackground = true;
            finder3.IsBackground = true;
            printerThread.IsBackground = true;

            finder1.Start();
            finder2.Start();
            finder3.Start();
            printerThread.Start();

            finder1.Join();
            finder2.Join();
            finder3.Join();

            printerThread.Join();

            Thread.Sleep(10000);
        }
    }
}

