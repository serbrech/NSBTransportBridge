using System;

namespace SurfLeague
{
    public class Organizer
    {
        private readonly Guid identification;

        private readonly string name;

        public Organizer(Guid identification, string name)
        {
            this.name = name;
            this.identification = identification;
        }

        public Guid Identification
        {
            get
            {
                return this.identification;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public bool CanRunCompetition(int windSpeed, int swellSize)
        {
            if (swellSize > 5)
            {
                return true;
            }
            return false;
        }
    }
}