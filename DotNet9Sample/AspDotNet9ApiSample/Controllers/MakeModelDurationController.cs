using AspDotNet9ApiSample.DTO;
using AspDotNet9ApiSample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MakeModelDurationController : ControllerBase
    {
        private readonly MakeModelDurationService _service;
        public MakeModelDurationController(MakeModelDurationService service) =>
            _service = service;

        // GET /api/makemodelduration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MakeModelDurationDto>>> Get()
            => Ok(await _service.GetByMakeModelAsync());
    }
}
