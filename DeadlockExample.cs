using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingPracticeConsole
{
    public class DeadlockExample
    {

        /*
         *  mutex is used for serializaed access to a resource / critical section of the code
         *  improper use of the mutex can result in dead locks
         *  mutex is different from Semaphor (when initialized with count 1) in essence that any other thread can also release the semaphor, and can be used for signalling
         *  mutex is different from Monitor (with aditional functionality) 
         */


        private Mutex lockA = new Mutex();
        private Mutex lockB = new Mutex();

        void Thread1()
        {
            lockA.WaitOne();
            Console.WriteLine(String.Format("Thread {0} acquires lock A", Thread.CurrentThread.Name));
            Thread.Sleep(1000);
            lockB.WaitOne();
            Console.WriteLine(String.Format("Thread {0} acquired both locks", Thread.CurrentThread.Name));
            lockB.ReleaseMutex();
            lockA.ReleaseMutex();
        }

        void Thread2()
        {
            lockB.WaitOne();
            Console.WriteLine(String.Format("Thread {0} acquires lock B", Thread.CurrentThread.Name));
            Thread.Sleep(1000);
            lockA.WaitOne();
            Console.WriteLine(String.Format("Thread {0} acquired both locks", Thread.CurrentThread.Name));
            lockA.ReleaseMutex();
            lockB.ReleaseMutex();
        }

        public void RunTest()
        {

            Thread t1 = new Thread(() =>
            {
                Thread1();
            });

            Thread t2 = new Thread(() =>
            {
                Thread2();
            });


            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

        }
    }
}
