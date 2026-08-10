using Microsoft.AspNetCore.Mvc;
using ShelterApi.DTOs;
using ShelterApi.Models;
using ShelterApi.Repositories;

namespace ShelterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SheltersController : ControllerBase
    {
        private readonly IShelterRepositories _shelterRepositories;
        public SheltersController(IShelterRepositories shelterRepositories)
        {
            _shelterRepositories = shelterRepositories;
        }
        [HttpGet("with-area")]
        public async Task<ActionResult<IEnumerable<ShelterWithAreaDto>>> shelterWithAreaDtos()
        {
            var result = await _shelterRepositories.GetSheltersAsync();
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ShelterSearchResultDto>>> Search(string? city, int? minCapacity, bool? isAccessible, bool? isPublic)
        {
            var result = await _shelterRepositories.GetSheltersByFiltersAsync(city, minCapacity, isAccessible, isPublic);
            return Ok(result);
        }
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<ShelterSortedDto>>> Search(string sortBy = "name", bool ascending = true)
        {
            var result = await _shelterRepositories.GetSheltersByFiltersAsync(sortBy, ascending);
            return Ok(result);
        }
        [HttpGet("with-inspection-count")]
        public async Task<ActionResult<IEnumerable<ShelterWithInspectionCountDto>>> GetShelterWithInspectionCount()
        {
            var result = await _shelterRepositories.GetSheltersWithInspectionCountAsync();
            return Ok(result);
        }
        [HttpGet("average-score-by-type")]
        public async Task<ActionResult<IEnumerable<ShelterTypeAverageDto>>> GetShelterTypeAverage()
        {
            var result = await _shelterRepositories.GetShelterTypeAverageAsync();
            return Ok(result);
        }
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto>> GetPage(int page = 1, int pageSize  = 10)
        {
            var result = await _shelterRepositories.GetShelterpagedAsync(page, pageSize);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ShelterDto>> createShelter(ShelterDetailDto shelter)
        {
            var result = await _shelterRepositories.createAnsyc(shelter);
            if (result == null)
            {
                return BadRequest("Could not create shelter.");
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> delete(int id)
        {
            var d = await _shelterRepositories.DeleteAsync(id);
            if(d == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
