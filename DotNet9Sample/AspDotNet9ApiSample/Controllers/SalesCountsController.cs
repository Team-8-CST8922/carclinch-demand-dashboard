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
    public class SalesCountsController : ControllerBase
    {
        private readonly SalesCountService _service;

        public SalesCountsController(SalesCountService service) =>
            _service = service;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            IEnumerable<SalesCountDto> results =
                await _service.GetSalesCountsAsync(from, to);
            return Ok(results);
        }
    }
}
