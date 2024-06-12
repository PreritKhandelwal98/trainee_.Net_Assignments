using System.IO;

namespace EmployeeManagementSystem.Entities
{
    public class Address
    {
        public int HouseOrBuildingNumber { get; set; }
        public string StreetName { get; set; }
        public string PostalCodes { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}
