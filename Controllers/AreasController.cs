using Microsoft.AspNetCore.Mvc;
using ShelterApi.DTOs;
using ShelterApi.Repositories;

namespace ShelterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaRepositories _areaRepositories;
        public AreasController(IAreaRepositories areaRepositories)
        {
            _areaRepositories = areaRepositories;
        }
        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<AreaStatisticsDto>>> GetAreaStatistics()
        {
            var result = await _areaRepositories.GetStatisticsByAreaAsync();
            return Ok(result);
        }
    }
}
