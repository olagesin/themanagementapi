namespace Models.DTOs
{
    public class GetCountryDto
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
