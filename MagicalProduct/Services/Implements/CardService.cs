using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Card;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Payload.Response.Card;
using MagicalProduct.API.Payload.Response.Payment;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.Repo.Interfaces;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.PortableExecutable;

namespace MagicalProduct.API.Services.Implements
{
    public class CardService : BaseService<CardService>, ICardService
    {
        private readonly IMapper _mapper;

        public CardService(IUnitOfWork unitOfWork, ILogger<CardService> logger, IMapper mapper) : base(unitOfWork, logger)
        {
            _mapper = mapper;
        }

        public BasicResponse Get(string? name, string? cardNum)
        {
            Expression<Func<Card, bool>> filter = null;
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(cardNum))
            {
                filter = p => p.NameOnCard.Contains(name) || p.CardNumber.Contains(cardNum);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                filter = p => p.NameOnCard.Contains(name);
            }
            else if (!string.IsNullOrWhiteSpace(cardNum))
            {
                filter = p => p.CardNumber.Contains(cardNum);
            }

            var objects = _unitOfWork.CardRepository.Get(filter: filter);
            var results = _mapper.Map<List<CardResponse>>(objects);

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get list successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = results.ToArray()
            };
            return response;

        }

        public BasicResponse GetById(int id)
        {
            var card = _unitOfWork.CardRepository
                .Get(filter: p => p.Id == id).FirstOrDefault();
            var result = _mapper.Map<CardResponse>(card);
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = result
            };
            return response;
        }
        public async Task<BasicResponse> Create(CardReq req)
        {
            var card = _mapper.Map<Card>(req);
            var lastItem = _unitOfWork.CardRepository.Get(orderBy: item => item.OrderByDescending(item => item.Id))
                .FirstOrDefault();
            card.Id = lastItem == null ? 1 : lastItem.Id + 1;
            card.User = _unitOfWork.UserRepository
                .Get(filter: p => p.Id == req.UserId).FirstOrDefault();
            card.PaymentMethod = _unitOfWork.PaymentMethodRepository
                .Get(filter: p => p.Id == req.PaymentMethodId).FirstOrDefault();
            _unitOfWork.CardRepository.Insert(card);
            await _unitOfWork.SaveAsync();
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = _mapper.Map<CardResponse>(card)
            };
            return response;
        }


        public async Task<BasicResponse> Update(int id, CardReq cardReq)
        {
            var existingCard = _unitOfWork.CardRepository.Get(filter: p => p.Id == id).FirstOrDefault();
            existingCard.User = _unitOfWork.UserRepository
                .Get(filter: p => p.Id == cardReq.UserId).FirstOrDefault();
            existingCard.PaymentMethod = _unitOfWork.PaymentMethodRepository
                .Get(filter: p => p.Id == cardReq.PaymentMethodId).FirstOrDefault();
            _mapper.Map(cardReq, existingCard);
            _unitOfWork.CardRepository.Update(existingCard);
            await _unitOfWork.SaveAsync();
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = _mapper.Map<CardResponse>(existingCard)
            };
            return response;
        }

        public async Task<BasicResponse> Delete(int id)
        {
            var existingCard = _unitOfWork.CardRepository.Get(filter: p => p.Id == id).FirstOrDefault();
            _unitOfWork.CardRepository.Delete(existingCard);
            await _unitOfWork.SaveAsync();
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = {}
            };
            return response;
        }
    }
}
