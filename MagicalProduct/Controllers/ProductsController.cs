using MagicalProduct.API.Payload.Request.Products;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagicalProduct.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        //[AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> Search([FromQuery] GetProductRequest getProductRequest)
        {
            var response = await _productService.SearchAsync(getProductRequest);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _productService.GetByIdAsync(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Create([FromForm] CreateRequest createReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.CreateAsync(createReq);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Update([FromForm] UpdateRequest updateReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productService.UpdateAsync(updateReq);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
