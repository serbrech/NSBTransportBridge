using System;

namespace Messages.Events
{
    public interface ICompetitionStarted
    {
        Guid Identification { get; set; }
        string Name { get; set; }
    }
}