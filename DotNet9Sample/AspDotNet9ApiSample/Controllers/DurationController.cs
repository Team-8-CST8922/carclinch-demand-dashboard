using AspDotNet9ApiSample.DTO;
using AspDotNet9ApiSample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DurationController : ControllerBase
    {
        private readonly DurationService _service;
        public DurationController(DurationService service) =>
            _service = service;

        // GET /api/duration/average
        [HttpGet("average")]
        public async Task<ActionResult<AverageDaysDto>> GetAverage()
            => Ok(await _service.GetAverageDaysInSystemAsync());
    }
}
