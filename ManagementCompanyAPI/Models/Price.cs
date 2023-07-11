namespace ManagementCompanyAPI.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public bool OnRequest { get; set; }
        public Location? Location { get; set; }
        public Catalog? Catalog { get; set; }
    }
}
