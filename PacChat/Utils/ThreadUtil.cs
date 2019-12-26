using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacChat.Utils
{
    public static class ThreadUtil
    {
        public static int GetWorkingThreads()
        {
            int maxThreads;
            int completionPortThreads;
            ThreadPool.GetMaxThreads(out maxThreads, out completionPortThreads);

            int availableThreads;
            ThreadPool.GetAvailableThreads(out availableThreads, out completionPortThreads);

            return maxThreads - availableThreads;
        }
    }
}
