
namespace Heaven
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
        }
    }
}
