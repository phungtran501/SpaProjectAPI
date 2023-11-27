﻿using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Enums;
using SpaManagement.Domain.Helper;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs.Product;

namespace SpaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageHandler _imageHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IImageHandler imageHandler, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _imageHandler = imageHandler;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> InsertUpdate([FromForm] ProductModel productModel)
        {
            var result = await _productService.CreateUpdate(productModel);
            if (result.Status && result.StatusType == StatusType.Success)
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                var path = Path.Combine(rootPath, "Image/product");
                var id = (int)result.Data;
                await _imageHandler.SaveImage(path, new List<IFormFile> { productModel.Image }, $"{id}.png");
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

        [HttpGet("product-by-service")]
        public async Task<IActionResult> GetProductByService(int id)
        {

            var products = await _productService.GetProductByService(id);

            return Ok(products);
        }

        [HttpGet("random-product")]
        public async Task<IActionResult> RandomProduct()
        {
            var products = await _productService.GetRandomProduct();

            return Ok(products);
        }

        [HttpGet("all-product")]
        public async Task<IActionResult> AllProduct(int pageIndex, int pageSize = 12)
        {
            var products = await _productService.AllProductPagination(pageIndex, pageSize);

            return Ok(products);
        }
    }
}
