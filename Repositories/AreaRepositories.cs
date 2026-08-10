using Microsoft.EntityFrameworkCore;
using ShelterApi.Data;
using ShelterApi.DTOs;
using ShelterApi.Models;

namespace ShelterApi.Repositories
{
    public class AreaRepositories : IAreaRepositories
    {
        private readonly ShelterDbContext _context;
        public AreaRepositories(ShelterDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<AreaStatisticsDto>> GetStatisticsByAreaAsync()
        {
            return await _context.Shelters
                .Include(a => a.Area)
                .GroupBy(a => a.Area.Neighborhood)
                .Select(s => new AreaStatisticsDto
                {
                    city = s.Select(a => a.Area.City).FirstOrDefault(),
                    neighborhood = s.Key,
                    shelterCount = s.Count(),
                    totalCapacity = s.Sum(a => a.Capacity)
                }).ToListAsync();
        }
    }
}
