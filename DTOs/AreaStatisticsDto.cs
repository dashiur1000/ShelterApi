namespace ShelterApi.DTOs
{
    public class AreaStatisticsDto
    {
        public string city {  get; set; }
        public string neighborhood { get; set; }
        public int shelterCount { get; set; }
        public int totalCapacity { get; set; }
    }
}
