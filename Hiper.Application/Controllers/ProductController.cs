using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hiper.Application.Domain.Services;
using Hiper.Application.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hiper.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> List() => Ok(await _service.List());

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Create([FromBody] ProductViewModel productViewModel) => Ok(await _service.Create(productViewModel));

        [HttpPut]
        public async Task<ActionResult<ProductViewModel>> Update([FromBody] ProductViewModel productViewModel) => Ok(await _service.Update(productViewModel));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
