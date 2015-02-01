using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace StartStopVirtualMachine
{
    public class Functions
    {
        public static void ProcessStartMessage([QueueTrigger("code-pushed")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }

        public static void ProcessStopMessage([QueueTrigger("sources-built")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }
    }
}
