using System;
using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Bridge.Msmq
{
    public class MsmqMessageHandler : IHandleMessages<ICanBeBridged>
    {
        static ILog log = LogManager.GetLogger<MsmqMessageHandler>();

        public Task Handle(ICanBeBridged message, IMessageHandlerContext context)
        {
            log.Info($"received {message.GetType()} in MsmqHandler. Forwarding.");
            return Program.Host.ToAzureEndpoint.Publish(message);
        }
    }

    //If I add this, it works.

    //public class DummyMessageHandler : IHandleMessages<ISwellSizeChanged>
    //{
    //    static ILog log = LogManager.GetLogger<MsmqMessageHandler>();

    //    public Task Handle(ISwellSizeChanged message, IMessageHandlerContext context)
    //    {
    //        return Task.FromResult(0);
    //    }
    //} 
}