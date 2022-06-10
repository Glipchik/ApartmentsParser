namespace ApartmentsParser.Domain.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int RoomsNumber { get; set; }
        public string Link { get; set; } = string.Empty;
    }
}