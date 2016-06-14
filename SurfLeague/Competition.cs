using System;

namespace SurfLeague
{
    public class Competition
    {
        public Competition(Guid guid, string name, int length)
        {
            Identification = guid;
            Name = name;
            Length = length;
            DaysToRun = length;
        }

        public Guid Identification { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int DaysToRun { get; set; }
        public bool IsOver
        {
            get { return DaysToRun == 0; }
        }

        public void Run()
        {
            Console.WriteLine("{0} days to go, and we can run today!", DaysToRun);
            Console.WriteLine("We can run today!");
            DaysToRun--;
            if (IsOver)
            {
                Console.WriteLine("The competition is over!! See you next time.");
            }
        }

        public void Pause()
        {
            Console.WriteLine("Not enough swell, it will be a lay day.");
        }
    }
}