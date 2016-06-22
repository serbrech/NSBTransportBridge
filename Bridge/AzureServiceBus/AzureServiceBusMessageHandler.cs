using System;
using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Bridge.AzureServiceBus
{
    public class AzureServiceBusMessageHandler : IHandleMessages<ICanBeBridged>
    {
        static ILog log = LogManager.GetLogger<AzureServiceBusMessageHandler>();

        public Task Handle(ICanBeBridged message, IMessageHandlerContext context)
        {
            log.Info($"received {message.GetType()} in AzureServiceBusMessageHandler. Forwarding.");
            return Program.Host.ToMsmqEndpoint.Publish(message);
        }
    }

    
}