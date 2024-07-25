using AutoMapper;
using MagicalProduct.API.Payload.Request.Categories;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MagicalProduct.API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        //[AuthorizePolicy(RoleEnum.Admin, RoleEnum.Staff)]
        public async Task<IActionResult> Search()
        {
            var response = await _categoryService.SearchAsync();
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var response = await _categoryService.CreateAsync(request);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request)
        {
            var response = await _categoryService.UpdateAsync(id, request);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        //[AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
