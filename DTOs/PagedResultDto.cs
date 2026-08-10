namespace ShelterApi.DTOs
{
    public class PagedResultDto
    {
        public List<ShelterDto> shelterDtos { get; set; } = new List<ShelterDto>();
        public int totalCount { get; set; }
        public int page {  get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
    }
}
