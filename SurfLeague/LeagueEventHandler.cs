using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Messages.Events;



namespace SurfLeague
{
    public class LeagueEventHandler : IHandleMessages<ISwellSizeChanged>
    {
        public Task Handle(ISwellSizeChanged message, IMessageHandlerContext context)
        {
            Console.WriteLine("Swell size has changed to {0}!", message.Size);
            return Task.Run(() => League.AnalyzeElements(message.Size, context));
        }

    }
}
