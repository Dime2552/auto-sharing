
using DAL.Enums;

namespace DAL.Entities
{
    public class Car
    {
        public required int Id { get; set; }
        public required string LicencePlate { get; set; }
        public required int Model { get; set; }
        public required string Year { get; set; }
        public required CarStatus Status { get; set; }
    }
}
