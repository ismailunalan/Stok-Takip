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

        //Gets all products
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _productService.GetAll();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
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
        public ActionResult<Product> Update(Product product)
        {
            var updatedProduct = _productService.Update(product);
            if (updatedProduct == null)
            {
                return NotFound("No product found to update.");
            }
            return Ok(updatedProduct);
        }
    }
}
