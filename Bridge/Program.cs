using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Messages.Events;
using Microsoft.WindowsAzure;
using NServiceBus;
using NServiceBus.AzureServiceBus;

namespace Bridge
{
    public class AzureMsmqBridgeHost
    {
        public IEndpointInstance ToAzureEndpoint { get; set; }
        public IEndpointInstance ToMsmqEndpoint { get; set; }

        public async Task Init()
        {
            //just making sure the assembly is loaded
            var type = typeof(ISwellSizeChanged);
            ToAzureEndpoint = await StartForwardToAzureEndpoint().ConfigureAwait(false);
            ToMsmqEndpoint = await StartForwardToMsmqEndpoint().ConfigureAwait(false);
        }

        private async Task<IEndpointInstance> StartForwardToMsmqEndpoint()
        {
            var endpointConfiguration = new EndpointConfiguration("AzureMsmqBridge.azure"); //we listen to  azure!

            endpointConfiguration.ExcludeTypes(
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.Contains("Bridge.Msmq")).ToArray());

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();

            endpointConfiguration.Conventions()
            .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Events"));

            transport.Queues().SupportOrdering(true);
            transport.UseTopology<ForwardingTopology>();
            transport.ConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("bridge.error");

            endpointConfiguration.UnicastRouting().AddPublisher("SurfLeague", typeof(ICanBeBridged));

            endpointConfiguration.Conventions()
             .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Events"));

            var endpoint = await Endpoint.Start(endpointConfiguration);
            await endpoint.Subscribe<ICanBeBridged>();
            return endpoint;
        }

        private async Task<IEndpointInstance> StartForwardToAzureEndpoint()
        {
            var endpointConfiguration = new EndpointConfiguration("AzureMsmqBridge.msmq"); //we listen to msmq!

            endpointConfiguration.ExcludeTypes(
                 Assembly.GetExecutingAssembly().GetTypes()
                 .Where(t => t.Namespace != null && t.Namespace.Contains("Bridge.AzureServiceBus")).ToArray());

            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("bridge.error");
        
            endpointConfiguration.UnicastRouting().AddPublisher("earth", typeof(ICanBeBridged));

            endpointConfiguration.Conventions()
             .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Events"));

            var endpoint = await  Endpoint.Start(endpointConfiguration);
            await endpoint.Subscribe<ICanBeBridged>();
            return endpoint;
        }

        public async Task StopAll()
        {
            if (ToMsmqEndpoint != null)
            {
                await ToMsmqEndpoint.Stop().ConfigureAwait(false);
            }
            if (ToAzureEndpoint != null)
            {
                await ToAzureEndpoint.Stop().ConfigureAwait(false);
            }
        }
    }

    public class Program
    {
        public static AzureMsmqBridgeHost Host;
        static void Main(string[] args)
        {
            try
            {
                Host = new AzureMsmqBridgeHost();
                Host.Init().GetAwaiter().GetResult();
                Console.ReadLine();
            }
            finally
            {
                Host.StopAll().GetAwaiter().GetResult();
            }
        }
    }
}
