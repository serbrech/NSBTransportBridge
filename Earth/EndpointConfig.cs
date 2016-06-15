
namespace Earth
{
    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseTransport<MsmqTransport>();

            configuration.Conventions()
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Events"));
        }
    }
}
