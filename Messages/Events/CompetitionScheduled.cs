using System;

namespace Messages.Events
{
    public class CompetitionStarted
    {
        public Guid Identification { get; set; }
        public string Name { get; set; }
    }

    public class CompetitionIsOver
    {
        public Guid Identification { get; set; }
    }
}