using ShelterApi.Models;
using ShelterApi.Data;
using Microsoft.EntityFrameworkCore;
using ShelterApi.DTOs;

namespace ShelterApi.Repositories
{
    public class InspectionRepositories : IInspectionRepositories
    {
        private readonly ShelterDbContext _context;
        public InspectionRepositories(ShelterDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<InspectionDetailedDto>> GetInspectionAsync()
        {
            return await _context.Inspections
                .Include(a => a.Shelter)
                .ThenInclude(a => a.Area)
                .Select(s => new InspectionDetailedDto
                {
                    inspectionId = s.Id,
                    inspectionDate = s.InspectionDate,
                    readinessScore = s.ReadinessScore,
                    passed = s.Passed,
                    shelterName = s.Shelter.Name,
                    city = s.Shelter.Area.City,
                    neighborhood = s.Shelter.Area.Neighborhood
                }).ToListAsync();
        }
        public async Task<ICollection<FailedInspectionDto>> GetFailedInspectionAsync()
        {
            return await _context.Inspections
                .Include (a => a.Shelter)
                .ThenInclude(a => a.Area)
                .Where(s => s.Passed == false)
                .Select (s => new FailedInspectionDto
                {
                    inspectionId = s.Id,
                    inspectionDate = s.InspectionDate,
                    readinessScore = s.ReadinessScore,
                    defectsCount = s.DefectsCount,
                    shelterName = s.Shelter.Name,
                    city = s.Shelter.Area.City
                }).ToListAsync ();
        }
    }
}
