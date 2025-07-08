using AspDotNet9ApiSample.DTO;
using AspDotNet9ApiSample.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonalCountsController : ControllerBase
    {
        private readonly SeasonalCountService _service;
        public SeasonalCountsController(SeasonalCountService service) =>
            _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] string? make = null,
            [FromQuery] string? model = null,
            [FromQuery] string? bodyType = null)
        {
            var data = await _service.GetSeasonalCountsAsync(from, to, make, model, bodyType);
            return Ok(data);
        }
    }
}
