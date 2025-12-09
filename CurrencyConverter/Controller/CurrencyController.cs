using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _service;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyService service, ILogger<CurrencyController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert(CurrencyRequest request)
        {
            _logger.LogInformation("Request received for currency conversion");

            var result = await _service.ConvertAsync(request);

            return Ok(result);
        }
    }
}
