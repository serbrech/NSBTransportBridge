using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using Messages.Events;

namespace Earth
{
    public class ElementsLottery : IWantToRunWhenBusStartsAndStops
    {
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        Random random = new Random();

        private Task task;

        public IBus Bus { get; set; }

        public void Start()
        {
            CancellationToken cancellationToken = this.tokenSource.Token;

            this.task = Task.Factory.StartNew(
                () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                   
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        Guid identification = Guid.NewGuid();
                        this.Bus.Publish<SwellSizeChanged>(m =>
                        {
                            m.Size = random.Next(0, 15);
                        });
                        Console.WriteLine("ElementsLottery : Swell size published");
                        cancellationToken.ThrowIfCancellationRequested();

                        Thread.Sleep(TimeSpan.FromSeconds(2));
                    }
                },
                cancellationToken);
        }

        public void Stop()
        {
            this.tokenSource.Cancel();

            try
            {
                this.task.Wait();
            }
            catch (AggregateException exception)
            {
                exception.Handle(ex => ex is OperationCanceledException);
            }
        }
    }
}
