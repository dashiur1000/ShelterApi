using ShelterApi.DTOs;

namespace ShelterApi.Repositories
{
    public interface IInspectionRepositories
    {
        Task<ICollection<InspectionDetailedDto>> GetInspectionAsync();
        Task<ICollection<FailedInspectionDto>> GetFailedInspectionAsync();
    }
}
