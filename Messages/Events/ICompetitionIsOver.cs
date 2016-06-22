using System;

namespace Messages.Events
{
    public interface ICompetitionIsOver
    {
        Guid Identification { get; set; }
    }
}