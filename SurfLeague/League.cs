using System;
using System.Collections.Concurrent;
using System.Linq;
using Messages.Events;
using NServiceBus;

namespace SurfLeague
{
    public static class League
    {
        private static Random random = new Random();
        private static Guid fijiGuid = Guid.NewGuid();
        private static ConcurrentDictionary<Guid, Competition> competitions = new ConcurrentDictionary<Guid, Competition>();
        private static Organizer[] organizers = new Organizer[] { new Organizer(Guid.NewGuid(), "Bryan Jones") };

        public static void AnalyzeElements(int swellSize, IMessageHandlerContext context)
        {
            Organizer organizer = organizers.First();
            Console.WriteLine("League: {0} is analyzing the elements.", organizer.Name);

            var competition = competitions.GetOrAdd(fijiGuid, new Competition(fijiGuid, "Fiji Pro", random.Next(1, 5)));

            if (organizer.CanRunCompetition(0, swellSize))
            {
                competition.Run();
                if (competition.IsOver)
                {
                    context.Publish<CompetitionIsOver>(over => over.Identification = fijiGuid);
                }
            }
            else
            {
                competition.Pause();
            }
        }
    }
}