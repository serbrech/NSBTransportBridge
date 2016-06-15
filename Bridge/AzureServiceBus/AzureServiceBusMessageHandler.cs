using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Bridge.AzureServiceBus
{
    public class AzureServiceBusMessageHandler : IHandleMessages<object>
    {
        static ILog log = LogManager.GetLogger<AzureServiceBusMessageHandler>();

        public Task Handle(object message, IMessageHandlerContext context)
        {
            log.Info($"received {message.GetType()} in AzureServiceBusMessageHandler. Forwarding.");
            return Program.Host.ToMsmqEndpoint.Publish(message);
        }
    }
}