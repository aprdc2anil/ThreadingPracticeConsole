using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingPracticeConsole
{
    public class ThreadPoolExample
    {
        EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);

        void Work(object state)
        {
            Thread.Sleep(5000);
            Console.WriteLine("Hello");
            ewh.Set();
        }

        public void RunTest()
        {
            ThreadPool.QueueUserWorkItem(Work);
            ewh.WaitOne();
        }
    }
}
