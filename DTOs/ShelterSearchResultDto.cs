namespace ShelterApi.DTOs
{
    public class ShelterSearchResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string street { get; set; }
        public int capacity { get; set; }
        public bool isAccessible { get; set; }
        public string city { get; set; }
    }
}
