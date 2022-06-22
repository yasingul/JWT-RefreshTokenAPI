using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryCacheAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMemoryCacheController : ControllerBase
    {
        [HttpGet(Name = "GetInteger")]
        public int Get()
        {
            var result = CacheModel.Get("testId");

            CacheModel.Delete("testId");

            result = CacheModel.Get("testId");

            return result;
        }
    }
}