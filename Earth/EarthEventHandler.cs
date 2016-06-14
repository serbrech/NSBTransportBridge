using System;
using Messages.Events;
using NServiceBus;

namespace Earth
{
    public class EarthEventHandler : IHandleMessages<SwellSizeChanged>
    {
       
        public IBus Bus { get; set; }
        public void Handle(SwellSizeChanged message)
        {
            Console.WriteLine("The swell size has changed to {0}!", message.Size);
        }
    }
}