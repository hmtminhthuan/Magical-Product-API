using MagicalProduct.Repo.Interfaces;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Models;
using MagicalProduct.API.Services.Interfaces;
using AutoMapper;

namespace MagicalProduct.API.Services.Implements
{
    public class NewsService : BaseService<NewsService>, INewsService
    {
        public NewsService(IUnitOfWork unitOfWork, ILogger<NewsService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<BasicResponse> GetAllNewsAsync()
        {
            var news = _unitOfWork.NewsRepository.Get();
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get all news successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = news.Select(n => new NewsResponse
                {
                    Id = n.Id,
                    Title = n.Title,
                    Thumbnail = n.Thumbnail,
                    Content = n.Content
                }).ToList()
            };
            return response;
        }

        public async Task<BasicResponse> GetNewsByIdAsync(int id)
        {
            var news = _unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "News ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get news by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new NewsResponse
                {
                    Id = news.Id,
                    Title = news.Title,
                    Thumbnail = news.Thumbnail,
                    Content = news.Content
                }
            };
            return response;
        }

        public async Task<BasicResponse> CreateNewsAsync(CreateNewsRequest createNewsRequest)
        {
            var lastItem = _unitOfWork.NewsRepository.Get(orderBy: item => item.OrderByDescending(item => item.Id))
                .FirstOrDefault(); 
            var newNews = new News
            {
                Id = lastItem == null ? 1 : lastItem.Id + 1,
                Title = createNewsRequest.Title,
                Thumbnail = createNewsRequest.Thumbnail,
                Content = createNewsRequest.Content
            };

            _unitOfWork.NewsRepository.Insert(newNews);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create new news successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = new NewsResponse
                {
                    Id = newNews.Id,
                    Title = newNews.Title,
                    Thumbnail = newNews.Thumbnail,
                    Content = newNews.Content
                }
            };
            return response;
        }

        public async Task<BasicResponse> UpdateNewsAsync(UpdateNewsRequest updateNewsRequest)
        {
            var news = _unitOfWork.NewsRepository.GetByID(updateNewsRequest.Id);
            if (news == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "News ID " + updateNewsRequest.Id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            news.Title = updateNewsRequest.Title;
            news.Thumbnail = updateNewsRequest.Thumbnail;
            news.Content = updateNewsRequest.Content;

            _unitOfWork.NewsRepository.Update(news);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update news successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new NewsResponse
                {
                    Id = news.Id,
                    Title = news.Title,
                    Thumbnail = news.Thumbnail,
                    Content = news.Content
                }
            };
            return response;
        }

        public async Task<BasicResponse> DeleteNewsAsync(int id)
        {
            var news = _unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "News ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            _unitOfWork.NewsRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete news successfully",
                StatusCode = StatusCodes.Status200OK
            };
            return response;
        }
    }
}
