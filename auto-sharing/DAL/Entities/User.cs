
namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string LicenceNumber { get; set; }
        public required string Password { get; set; }
    }
}
