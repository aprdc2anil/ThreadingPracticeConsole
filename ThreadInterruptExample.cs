using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingPracticeConsole
{
    public class ThreadInterruptExample
    {

        void ChildThread()
        {

            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("caught exception");
            }
            finally
            {
                // empty block
            }
            Console.WriteLine("Child thread exiting");

        }


        public void RunTest()
        {

            Thread child = new Thread(() =>
            {
                ChildThread();
            });

            child.Start();

            // wait for child thread to block on Sleep

            Thread.Sleep(1000);

            // now interrupt the child thread
            child.Interrupt();

            // wait for child thread to finish
            child.Join();

            Console.WriteLine("Main thread exiting");
        }
    }

}

