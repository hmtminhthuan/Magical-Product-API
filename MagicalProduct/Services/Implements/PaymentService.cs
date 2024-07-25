using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Payment;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Payload.Response.Payment;
using MagicalProduct.API.Services.Interfaces;
using MagicalProduct.Repo.Interfaces;
using Newtonsoft.Json.Linq;

namespace MagicalProduct.API.Services.Implements
{
    public class PaymentService : BaseService<PaymentService>, IPaymentService
    {
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, ILogger<PaymentService> logger, IMapper mapper) : base(unitOfWork, logger)
        {
            _mapper = mapper;
        }

        public async Task<BasicResponse> GetPaymentMethods(string? type)
        {
            IEnumerable<PaymentMethod> payments;

            if (!string.IsNullOrEmpty(type))
            {
                payments = _unitOfWork.PaymentMethodRepository
                    .Get(filter: p => p.PaymentType.Equals(type));
            }
            else
            {
                payments = _unitOfWork.PaymentMethodRepository.Get();
            }
            var responses = _mapper.Map<IEnumerable<PaymentResponse>>(payments);

            var res = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get all payment methods successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = responses.ToArray()
            };
            return res;
        }

        public async Task<BasicResponse> GetPaymentMethodById(int id)
        {
            var payment = _unitOfWork.PaymentMethodRepository
                .Get(filter: p => p.Id == id).FirstOrDefault();
            var responses = _mapper.Map<PaymentResponse>(payment);

            var res = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = responses
            };
            return res;
        }


        public async Task<BasicResponse> CreatePaymentMethod(PaymentReq request)
        {
            var lastItem = _unitOfWork.PaymentMethodRepository.Get(orderBy: item => item.OrderByDescending(item => item.Id))
                .FirstOrDefault();
            PaymentMethod p = new PaymentMethod();
            p.Id = lastItem == null ? 1 : lastItem.Id + 1;
            p.PaymentType = request.PaymentType;
            _unitOfWork.PaymentMethodRepository.Insert(p);
            _unitOfWork.Save();
            return new BasicResponse
            {
                IsSuccess = true,
                Message = "Create successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = _mapper.Map<PaymentResponse>(p)
            };
        }

        public async Task<BasicResponse> UpdatePaymentMethod(PaymentResponse request)
        {
            var payment = _unitOfWork.PaymentMethodRepository
                .Get(filter: p => p.Id == request.Id).FirstOrDefault();

            payment.PaymentType = request.PaymentType;
            _unitOfWork.PaymentMethodRepository.Update(payment);
            _unitOfWork.Save();
            return new BasicResponse
            {
                IsSuccess = true,
                Message = "Update successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = payment
            };
        }
        public async Task<BasicResponse> DeletePaymentMethod(int id)
        {
            var payment = _unitOfWork.PaymentMethodRepository
                .Get(filter: p => p.Id == id).FirstOrDefault();
            _unitOfWork.PaymentMethodRepository.Delete(payment);
            _unitOfWork.Save();
            return new BasicResponse
            {
                IsSuccess = true,
                Message = "Delete successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = {}
            };
        }
    }
}
