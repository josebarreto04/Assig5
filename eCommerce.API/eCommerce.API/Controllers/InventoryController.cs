using Amazon.Library.Models;
using eCommerce.API.EC;
using eCommerce.Library.DTO;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await new InventoryEC().Get();
        }
        [HttpPost()]
        public async Task<ProductDTO> AddorUpdate([FromBody] ProductDTO p)
        {
            return await new InventoryEC().AddorUpdate(p);
        }
        [HttpDelete("/{id}")]
        public async Task<ProductDTO> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }
        [HttpGet("Search")]
        public async Task<IEnumerable<ProductDTO>> Get(Query query)
        {
            return await new InventoryEC().Search(query.QueryString);
        }
    }
}
