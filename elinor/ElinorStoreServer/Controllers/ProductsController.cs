using ElinorStoreServer.Data.Entities;
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
