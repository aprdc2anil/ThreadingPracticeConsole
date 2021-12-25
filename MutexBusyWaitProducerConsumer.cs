﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingPracticeConsole
{
    public class MutexBusyWaitProducerConsumer
    {
        private Mutex lockProducer = new Mutex();
        private Mutex lockConsumer = new Mutex();
        private int remainingCount = 1;
        private Random random = new Random();

        void Producer()
        {
            for (int i = 1; i <= 5; ++i)
            {
                lockProducer.WaitOne();

                while (remainingCount > 0)
                {
                    lockProducer.ReleaseMutex();
                    Thread.Sleep(random.Next(500, 1000));
                    lockProducer.WaitOne();
                    Console.WriteLine(String.Format("Producer {0} waiting for items to be consumed, remaining items - {1}, iteration - {2}  Released the producer mutex ", Thread.CurrentThread.Name, remainingCount, i));
                }

                // lockConsumer.WaitOne();
                ++remainingCount;
               // lockConsumer.ReleaseMutex();

                Console.WriteLine(String.Format("Producer {0} produced item, remaining items - {1}, iteration {2} ", Thread.CurrentThread.Name, remainingCount, i));

                lockProducer.ReleaseMutex();
            }
        }

        void Consumer()
        {
            for (int i = 1; i <= 5; ++i)
            {
                lockConsumer.WaitOne();

                while (remainingCount == 0)
                {
                    lockConsumer.ReleaseMutex();
                    Thread.Sleep(random.Next(500, 1000));
                    lockConsumer.WaitOne();
                    Console.WriteLine(String.Format("Consumer {0} waiting for items to be produced, remaining items - {1}, iteration - {2} Released the consumer mutex ", Thread.CurrentThread.Name, remainingCount, i));
                }

               // lockProducer.WaitOne();
                --remainingCount;
               // lockProducer.ReleaseMutex();

                Console.WriteLine(String.Format("Consumer {0} consumed item, remaining items - {1}, iteration - {2} ", Thread.CurrentThread.Name, remainingCount, i));

                lockConsumer.ReleaseMutex();
            }
        }

        public void RunTest()
        {

            Thread t1 = new Thread(() =>
            {
                Producer();
            });

            t1.Name = "Producer1";


            Thread t2 = new Thread(() =>
            {
                Consumer();
            });

            t2.Name = "Consumer1";

            Thread t3 = new Thread(() =>
            {
                Producer();
            });

            t3.Name = "Producer2";

            Thread t4 = new Thread(() =>
            {
                Consumer();
            });

            t4.Name = "Consumer2";

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            Console.ReadLine();
        }
    }
}
