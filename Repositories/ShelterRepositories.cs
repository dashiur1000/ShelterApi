using ShelterApi.Models;
using ShelterApi.Data;
using Microsoft.EntityFrameworkCore;
using ShelterApi.DTOs;
using System.Collections.Immutable;

namespace ShelterApi.Repositories
{
    public class ShelterRepositories : IShelterRepositories
    {
        private readonly ShelterDbContext _context;
        public ShelterRepositories(ShelterDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<ShelterWithAreaDto>> GetSheltersAsync()
        {
            return await _context.Shelters
                .Include(a => a.Area)
                .Select(s => new ShelterWithAreaDto
                {
                    ShelterId = s.Id,
                    ShelterName = s.Name,
                    Capacity = s.Capacity,
                    City = s.Area.City,
                    Neighborhood = s.Area.Neighborhood
                }).ToListAsync();
        }
        public async Task<ICollection<ShelterSearchResultDto>> GetSheltersByFiltersAsync(string? city, int? minCapacity, bool? isAccessible, bool? isPublic)
        {
            var result = _context.Shelters.Include(a => a.Area).AsQueryable();
            if(!string.IsNullOrEmpty(city))
            {
                result = result.Where(a => a.Area.City.ToLower() == city.ToLower());
            }
            if (minCapacity != null)
            {
                result = result.Where(a => a.Capacity >= minCapacity);
            }
            if(isAccessible != null)
            {
                result = result.Where(a => a.IsAccessible == isAccessible);
            }
            if(isPublic != null)
            {
                result = result.Where(a => a.IsPublic == isPublic);
            }
            return await result
                .Select(s => new ShelterSearchResultDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    street = s.Street,
                    capacity = s.Capacity,
                    isAccessible = s.IsAccessible,
                    city = s.Area.City
                }).ToListAsync();
        }
        public async Task<ICollection<ShelterSortedDto>> GetSheltersByFiltersAsync(string sortBy, bool ascending)
        {
            var query = _context.Shelters.Include(a => a.Area).AsQueryable();
            if (ascending)
            {
                if (sortBy.ToLower().Trim().Equals("city"))
                {
                    query = query.OrderBy(a => a.Area.City);
                }
                else if (sortBy.ToLower().Trim().Equals("capacity"))
                {
                    query = query.OrderBy(a => a.Capacity);
                }
                if(sortBy.ToLower().Trim().Equals("name"))
                    query = query.OrderBy(a => a.Name);
            }
            else
            {
                if (sortBy.ToLower().Trim().Equals("city"))
                {
                    query = query.OrderByDescending(a => a.Area.City);
                }
                if (sortBy.ToLower().Trim().Equals("capacity"))
                {
                    query = query.OrderByDescending(a => a.Capacity);
                }
                else if (sortBy.ToLower().Trim().Equals("name"))
                {
                    query = query.OrderByDescending(a => a.Name);
                }
            }
            return await query
                .Select(a => new ShelterSortedDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Capacity = a.Capacity,
                    City = a.Area.City,
                }).ToListAsync();
            
        }
        public async Task<ICollection<ShelterWithInspectionCountDto>> GetSheltersWithInspectionCountAsync()
        {
            var query = _context.Shelters.Include(a => a.Inspections)
                .Select(a => new ShelterWithInspectionCountDto
                {
                    shelterId = a.Id,
                    shelterName = a.Name,
                    inspectionCount = a.Inspections.Count()
                }).ToListAsync();
            return await query;
        }
        public async Task<ICollection<ShelterTypeAverageDto>> GetShelterTypeAverageAsync()
        {
            return await _context.Inspections
                .Include(a => a.Shelter)
                .GroupBy(a => a.Shelter.ShelterType)
                .Select(s => new ShelterTypeAverageDto
                {
                    shelterType = s.Key,
                    averageReadinessScore = s.Average(s => s.ReadinessScore),
                    totalInspections = s.Count()
                }).ToListAsync();
        }
        public async Task<PagedResultDto> GetShelterpagedAsync(int page = 1, int pageSize = 10)
        {
            if(page < 1 || pageSize < 5 || pageSize > 50)
            {
                return null;
            }
            var totalCount = await _context.Shelters.CountAsync();
            var totalPages = (int)Math.Ceiling((double)_context.Shelters.Count() / pageSize);
            var jump = (page-1)*pageSize;

            var shelters = await _context.Shelters
                .OrderBy(a => a.Name)
                .Skip(jump)
                .Take(pageSize)
                .Select(a => new ShelterDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    capacity = a.Capacity,
                }).ToListAsync();

                return new PagedResultDto
                {
                    shelterDtos = shelters,
                    totalCount = totalCount,
                    page = page,
                    pageSize = pageSize,
                    totalPages = totalPages
                };
            
        }
        public async Task<ShelterDto> createAnsyc(ShelterDetailDto shelter)
        {
            var area = await _context.Areas.FindAsync(shelter.AreaId);
            if(area == null)
            {
                return null;
            }
            var newShelter = new Shelter
            {
                Name = shelter.Name,
                Street = shelter.Street,
                BuildingNumber = shelter.BuildingNumber,
                Capacity = shelter.Capacity,
                IsAccessible = shelter.IsAccessible,
                IsPublic = shelter.IsPublic,
                ShelterType = shelter.ShelterType,
                Area = area
            };
            _context.Shelters.Add(newShelter);
            await _context.SaveChangesAsync();
            return new ShelterDto
            {
                Id = newShelter.Id,
                Name = newShelter.Name,
                capacity = newShelter.Capacity
            };
            
        }
        public async Task<Shelter> DeleteAsync(int id)
        {
            var toDelete = _context.Shelters.Where(a => a.Id == id).FirstOrDefault();
            if(toDelete == null)
            {
                return null;
            }
            _context.Shelters.Remove(toDelete);
            await _context.SaveChangesAsync();
            return toDelete;
        }



    }
}
