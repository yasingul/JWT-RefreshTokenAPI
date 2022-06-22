using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text;
using System.Text.Json;

namespace RedisDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "Lenovo Laptop",
            Price = 175000.00,
            Quantity = 150
        },
        new Product
        {
            Id = 2,
            Name = "DELL Laptop",
            Price = 185000.00,
            Quantity = 250
        },
        new Product
        {
            Id = 3,
            Name = "HP Laptop",
            Price = 195500.00,
            Quantity = 200
        }
    };

        private readonly IDistributedCache _cache;

        public ProductController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string? serializedData = null;

            var dataAsByteArray = await _cache.GetAsync("products");

            if ((dataAsByteArray?.Count() ?? 0) > 0)
            {
                serializedData = Encoding.UTF8.GetString(dataAsByteArray);
                var products = JsonSerializer.Deserialize
                    <List<Product>>(serializedData);

                return Ok(new ProductResponse()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsDataFromCache = true,
                    Data = products,
                    Message = "Data retrieved from Redis Cache",
                    Timestamp = DateTime.UtcNow
                });
            }

            serializedData = JsonSerializer.Serialize(products);
            dataAsByteArray = Encoding.UTF8.GetBytes(serializedData);
            await _cache.SetAsync("products", dataAsByteArray);

            ProductResponse productResponse = new ProductResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsDataFromCache = false,
                Data = products,
                Message = "Data not available in Redis Cache",
                Timestamp = DateTime.UtcNow
            };

            var expiration = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(25)
            };
            return Ok(productResponse);
        }
    }
}