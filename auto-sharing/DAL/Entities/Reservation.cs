
using DAL.Enums;

namespace DAL.Entities
{
    public class Reservation
    {
        public required int Id { get; set; }
        public required int CarId { get; set; }
        public required ReservationStatus Status { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required int UserId { get; set; }
    }
}
