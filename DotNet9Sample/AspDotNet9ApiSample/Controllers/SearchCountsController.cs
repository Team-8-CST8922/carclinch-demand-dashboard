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
            [FromQuery] DateTime to)
        {
            IEnumerable<SearchCountDto> results =
                await _service.GetSearchCountsAsync(from, to);
            return Ok(results);
        }
    }
}
