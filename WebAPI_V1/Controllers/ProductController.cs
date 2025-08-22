using Microsoft.AspNetCore.Mvc;
using WebAPI_V1.Models;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("Cal")]
        public object CC()
        {
            return new {a = 5};
        }
        
    }
}
