namespace ShelterApi.DTOs
{
    public class ShelterWithAreaDto
    {
        public int ShelterId { get; set; }
        public string? ShelterName { get; set; }
        public int Capacity { get; set; }
        public string? City { get; set; }
        public string? Neighborhood { get; set; }
    }
}
