namespace Lab_4.Domain.Entity
{
    public class HumanEntity
    {
        public string FirstName { get; }
        public string LastName { get; }
        public long PassportNumber { get; }
        public string PassportSeries { get; }
        public string HomeTown { get; }

        public HumanEntity(string firstName, string lastName, long passportNumber, string passportSeries,
            string homeTown)
        {
            FirstName = firstName;
            LastName = lastName;
            PassportNumber = passportNumber;
            PassportSeries = passportSeries;
            HomeTown = homeTown;
        }
    }
}