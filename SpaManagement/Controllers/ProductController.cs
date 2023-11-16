using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.EmailHelper;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Domain.Model;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> Index(int page, int per_page)
        {
            var products = await _productService.GetAllProduct(page, per_page);

            return Ok(products);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetProductDetail(int id)
        {

            var product = await _productService.GetProductById(id);

            return Ok(product);
        }

        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] ProductModel productModel)
        {
            var result = await _productService.CreateUpdate(productModel);
            if (result.Status && result.StatusType == StatusType.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.DeleteProduct(productId);
            return Ok(true);
        }
    }
}
