namespace ManagementCompanyAPI.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Company? Company { get; set; }
    }
}