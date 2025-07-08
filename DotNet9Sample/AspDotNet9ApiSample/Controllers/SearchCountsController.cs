using AspDotNet9ApiSample.DTO;
using AspDotNet9ApiSample.Services;
using Microsoft.AspNetCore.Mvc;
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

        // GET /api/searchcounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchCountDto>>> GetSuvCounts()
            => Ok(await _service.GetSuvCountsAsync());

        // GET /api/searchcounts/annual
        [HttpGet("annual")]
        public async Task<ActionResult<IEnumerable<SearchCountDto>>> GetAnnualSuv()
            => Ok(await _service.GetAnnualSuvCountsAsync());
    }
}
