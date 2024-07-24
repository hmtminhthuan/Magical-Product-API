using Microsoft.AspNetCore.Mvc;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Services.Interfaces;
using System.Threading.Tasks;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Middlewares;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/v1/news")]
    public class NewsController : BaseController<NewsController>
    {
        private readonly INewsService _newsService;

        public NewsController(ILogger<NewsController> logger, INewsService newsService)
            : base(logger)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var response = await _newsService.GetAllNewsAsync();
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var response = await _newsService.GetNewsByIdAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPost]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsRequest createNewsRequest)
        {
            var response = await _newsService.CreateNewsAsync(createNewsRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPut("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] UpdateNewsRequest updateNewsRequest)
        {
            updateNewsRequest.Id = id;
            var response = await _newsService.UpdateNewsAsync(updateNewsRequest);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpDelete("{id}")]
        [AuthorizePolicy(RoleEnum.Admin)]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var response = await _newsService.DeleteNewsAsync(id);
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
