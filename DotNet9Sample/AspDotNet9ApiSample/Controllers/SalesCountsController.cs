using AspDotNet9ApiSample.DTO;
using AspDotNet9ApiSample.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<SalesCountDto>>> Get()
        {
            var data = await _service.GetSalesCountsAsync();
            return Ok(data);
        }
    }
}
