using System;
using System.Threading.Tasks;
using Hiper.Application.Domain.Services;
using Hiper.Application.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hiper.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly StockService _service;

        public StockController(StockService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPut]
        public async Task<ActionResult<StockViewModel>> Update([FromBody] StockViewModel stockViewModel) => Ok(await _service.Update(stockViewModel));

    }
}
