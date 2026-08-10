using Microsoft.AspNetCore.Mvc;
using ShelterApi.DTOs;
using ShelterApi.Models;
using ShelterApi.Repositories;

namespace ShelterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspectionsController : ControllerBase
    {
        private readonly IInspectionRepositories _inspectionRepositories;
        public InspectionsController(IInspectionRepositories inspectionRepositories)
        {
            _inspectionRepositories = inspectionRepositories;
        }
        [HttpGet("detailed")]
        public async Task<ActionResult<IEnumerable<InspectionDetailedDto>>> GetAllInspection()
        {
            var result = await _inspectionRepositories.GetInspectionAsync();
            return Ok(result);
        }
        [HttpGet("failed")]
        public async Task<ActionResult<IEnumerable<FailedInspectionDto>>> GetFailedInspection()
        {
            var result = await _inspectionRepositories.GetFailedInspectionAsync();
            return Ok(result);
        }

    }
}
