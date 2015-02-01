using Microsoft.Azure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StartStopVirtualMachine
{
    public class VirtualMachineService
    {
        private ComputeManagementClient client;
        private string serviceName;
        private string deploymentName;
        private string virtualMachineName;

        public VirtualMachineService()
        {
            serviceName = System.Configuration.ConfigurationManager.AppSettings["serviceName"];
            deploymentName = System.Configuration.ConfigurationManager.AppSettings["deploymentName"];
            virtualMachineName = System.Configuration.ConfigurationManager.AppSettings["virtualMachineName"];

            var subscriptionId = System.Configuration.ConfigurationManager.AppSettings["subscriptionId"];
            var certificate = System.Configuration.ConfigurationManager.AppSettings["base64EncodedCertificate"];
            client = new ComputeManagementClient(GetCredentials(subscriptionId, certificate));
        }

        public async Task Stop()
        {
            var operationStatusResponse = client.VirtualMachines.Shutdown(serviceName, deploymentName, virtualMachineName, new VirtualMachineShutdownParameters());
        }

        public async Task Start()
        {
            var operationStatusResponse = client.VirtualMachines.Start(serviceName, deploymentName, virtualMachineName);
        }
        private static SubscriptionCloudCredentials GetCredentials(string subscriptionId, string base64EncodedCertificate)
        {
            return new CertificateCloudCredentials(subscriptionId, new X509Certificate2(Convert.FromBase64String(base64EncodedCertificate)));
        }
    }
}
