using Microsoft.AspNetCore.Mvc;
using WebAPI_V1.Models;
using WebAPI_V1.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/Products/[action]")]

    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll(
    [FromQuery] string sortColumn = "Code",
    [FromQuery] bool ascending = true,
    [FromQuery] string? searchColumn = null,
    [FromQuery] string? searchText = null)
        {
            var products = _productService.GetAll(sortColumn, ascending, searchColumn, searchText);

            if (products == null || !products.Any())
            {
                return Ok(new List<Product>());
            }

            return Ok(products);
        }

        [HttpGet]
        public ActionResult<Product> Get(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound("No product found.");
            }
            return Ok(product);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var deleted = _productService.Delete(id);
            if (!deleted)
            {
                return NotFound("No product found to delete.");
            }
            return NoContent();
        }

        [HttpPut] 
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("URL ID does not match product ID.");
            }
            bool success = _productService.Update(product);
            if (!success)
            {
                return NotFound("Product not found or update failed.");
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }
            bool success = _productService.Create(product);
            if (!success)
            {
                return StatusCode(500, "Failed to create product.");
            }
            return Ok("Product created successfully.");
        }


    }
}
