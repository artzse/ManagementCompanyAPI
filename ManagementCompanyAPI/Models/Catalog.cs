namespace ManagementCompanyAPI.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public string? Units { get; set; }
        public Company? Company { get; set; }
    }
}
