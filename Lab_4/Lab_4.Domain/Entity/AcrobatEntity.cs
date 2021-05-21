using System;
using Lab_4.Domain.Abstraction;

namespace Lab_4.Domain.Entity
{
    public class AcrobatEntity : HumanEntity, IDancer
    {
        public AcrobatEntity(string firstName, string lastName, long passportNumber, string passportSeries,
            string homeTown) : base(firstName, lastName, passportNumber, passportSeries, homeTown)
        {
        }

        public void Dance()
        {
            Console.WriteLine("Dancing");
        }
    }
}