
using System.Configuration;
using Microsoft.WindowsAzure;

namespace SurfLeague
{
    using NServiceBus;
    using NServiceBus.AzureServiceBus;


    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            var transport = configuration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            transport.UseTopology<ForwardingTopology>();
            configuration.SendFailedMessagesTo("error");
            configuration.AuditProcessedMessagesTo("audit");

            configuration.Conventions()
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Events"))
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Commands"));
        }
    }
}
