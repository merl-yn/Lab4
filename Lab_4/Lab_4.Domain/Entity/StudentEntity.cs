namespace Lab_4.Domain.Entity
{
    public class StudentEntity : HumanEntity
    {
        public int Class { get; }
        public string StudentTicket { get; }

        public StudentEntity(string firstName, string lastName, string studentTicket, long passportNumber,
            string passportSeries, int @class, string homeTown) : base(firstName, lastName, passportNumber,
            passportSeries, homeTown)
        {
            StudentTicket = studentTicket;
            Class = @class;
        }
    }
}