using ShelterApi.DTOs;

namespace ShelterApi.Repositories
{
    public interface IAreaRepositories
    {
        Task<ICollection<AreaStatisticsDto>> GetStatisticsByAreaAsync();
    }
}
