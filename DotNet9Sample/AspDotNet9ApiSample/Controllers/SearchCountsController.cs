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
    public class SearchCountsController : ControllerBase
    {
        private readonly SearchCountService _service;

        public SearchCountsController(SearchCountService service) =>
            _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] string? make = null,
            [FromQuery] string? model = null,
            [FromQuery] string? bodyType = null)
        {
            var results = await _service.GetSearchCountsAsync(
                from, to, make, model, bodyType);
            return Ok(results);
        }
    }
}
