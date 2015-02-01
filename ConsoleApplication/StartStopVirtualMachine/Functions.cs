using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Management.Compute.Models;
using System.Security.Cryptography.X509Certificates;

namespace StartStopVirtualMachine
{
    public class Functions
    {
        public static void ProcessStartMessage([QueueTrigger("code-pushed")] string message, TextWriter log)
        {
            log.WriteLine(message);
            new VirtualMachineService().Start().Wait();
            log.WriteLine("Virtual machine restarting...");
        }

        public static void ProcessStopMessage([QueueTrigger("sources-built")] string message, TextWriter log)
        {
            log.WriteLine(message);
            new VirtualMachineService().Stop().Wait();
            log.WriteLine("Virtual machine restarted.");
        }
    }
}
