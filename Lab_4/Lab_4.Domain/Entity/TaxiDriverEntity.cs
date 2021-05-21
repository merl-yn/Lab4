using System;

namespace Lab_4.Domain.Entity
{
    public class TaxiDriverEntity : HumanEntity
    {
        public string DriverLicenseId { get; private set; }
        
        public TaxiDriverEntity(string firstName, string lastName, long passportNumber, string passportSeries,
            string homeTown, string driverLicenseId = null) : base(firstName, lastName, passportNumber, passportSeries, homeTown)
        {
            DriverLicenseId = driverLicenseId;
        }

        public void GetDriverLicense()
        {
            DriverLicenseId = Guid.NewGuid().ToString();
        }
    }
}