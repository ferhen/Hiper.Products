using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiper.SynchronizationAPI.Domain.Services;
using Hiper.SynchronizationAPI.Presentation.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiper.SynchronizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSnapshotController : ControllerBase
    {
        private readonly ProductSnapshotService _service;

        public ProductSnapshotController(ProductSnapshotService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSnapshotViewModel>>> List() => Ok(await _service.List());
    }
}
