using AspDotNet9ApiSample.Interfaces;
using AspDotNet9ApiSample.Requests;
using AspDotNet9ApiSample.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNet9ApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        }

        [HttpPost]
        public async Task<CarCountByDateResponse> GetCountAsync([FromBody] CarCountByDateRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (!request.Date.HasValue)
            {
                return new CarCountByDateResponse
                {
                    ErrorMessage = "Date is required.",
                    Success = false
                };
            }

            try
            {
                var response = await _carService.GetCarCountByDateAsync(request.Date.Value, cancellationToken);

                return new CarCountByDateResponse
                {
                    response = response,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CarCountByDateResponse
                {
                    response = null,
                    ErrorMessage = ex.Message,
                    Success = false
                };
            }
        }
    }
}
