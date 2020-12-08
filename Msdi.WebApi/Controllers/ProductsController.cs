using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Msdi.Business.Abstract;
using Msdi.Entities.Concrete;
using Msdi.ViewModels.DTOs;
using System.Collections.Generic;

namespace Msdi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var result = _productService.GetProducts();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var result = _productService.GetProduct(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }


            var result = _productService.UpdateProduct(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostProduct(ProductDTO productDTO)
        {
            var result = _productService.AddProduct(productDTO);
            if (result.Success)
            {
                return Created("",result.Message);
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Product product)
        {
            var result = _productService.DeleteProduct(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return NoContent();
        }
    }
}
