
namespace Earth
{
    using NServiceBus;
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseTransport<MsmqTransport>();
        }
    }
}
