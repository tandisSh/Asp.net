/*using ElinorStoreServer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElinorStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] ProductsNames = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpGet("GetProducts")]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                Name= ProductsNames[Random.Shared.Next(ProductsNames.Length)],
                CreatedAt = DateTime.Now.AddDays(-index),
                Id = index,
                Description = ProductsNames[Random.Shared.Next(ProductsNames.Length)],
                ImageFileName = ProductsNames[Random.Shared.Next(ProductsNames.Length)],
                Price = index*1000

            })
            .ToArray();
        }
    }
}
*/
using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using share.Models.Product;



namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _productService.GetsAsync();
            return Ok(result);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] OrderSearchRequestDto model)
        {
            var result = await _productService.SearchAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddRequestDto product)
        {
            await _productService.AddAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Product product)
        {
            await _productService.EditAsync(product);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}