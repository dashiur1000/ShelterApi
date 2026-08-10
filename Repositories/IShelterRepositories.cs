using ShelterApi.DTOs;
using ShelterApi.Models;

namespace ShelterApi.Repositories
{
    public interface IShelterRepositories
    {
        Task<ICollection<ShelterWithAreaDto>> GetSheltersAsync();
        Task<ICollection<ShelterSearchResultDto>> GetSheltersByFiltersAsync(string? city, int? minCapacity, bool? isAccessible, bool? isPublic);
        Task<ICollection<ShelterSortedDto>> GetSheltersByFiltersAsync(string sortBy, bool ascending);
        Task<ICollection<ShelterWithInspectionCountDto>> GetSheltersWithInspectionCountAsync();
        Task<ICollection<ShelterTypeAverageDto>> GetShelterTypeAverageAsync();
        Task<PagedResultDto> GetShelterpagedAsync(int page = 1, int pageSize = 10);
        Task<ShelterDto> createAnsyc(ShelterDetailDto shelter);
        Task<Shelter> DeleteAsync(int id);
    }
}
