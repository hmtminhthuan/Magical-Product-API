using MagicalProduct.Repo.Interfaces;
using MagicalProduct.API.Payload.Request;
using MagicalProduct.API.Payload.Response;
using MagicalProduct.API.Enums;
using MagicalProduct.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicalProduct.API.Models;
using MagicalProduct.API.Services.Interfaces;
using AutoMapper;

namespace MagicalProduct.API.Services.Implements
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<BasicResponse> GetAllOrdersAsync()
        {
            var orders = _unitOfWork.OrderRepository.Get(includeProperties: "OrderDetails");
            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get all orders successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = orders.Select(o => new OrderResponse
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount ?? 0,
                    Address = o.Address,
                    Status = o.Status ?? 0,
                    CreateAt = o.CreateAt ?? DateTime.MinValue,
                    PaymentMethodId = o.PaymentMethodId ?? 0,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Amount = od.Amount ?? 0,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                }).ToList()
            };
            return response;
        }

        public async Task<BasicResponse> GetOrderByIdAsync(int id)
        {
            var order = _unitOfWork.OrderRepository.GetByID(id);
            if (order == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Order ID " + id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Get order by ID successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new OrderResponse
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TotalAmount = order.TotalAmount ?? 0,
                    Address = order.Address,
                    Status = order.Status ?? 0,
                    CreateAt = order.CreateAt ?? DateTime.MinValue,
                    PaymentMethodId = order.PaymentMethodId ?? 0,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Amount = od.Amount ?? 0,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                }
            };
            return response;
        }

        public async Task<BasicResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
        {
            var user = _unitOfWork.UserRepository.GetByID(createOrderRequest.UserId);
            if (user == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "User ID " + createOrderRequest.UserId + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var paymentMethod = _unitOfWork.PaymentMethodRepository.GetByID(createOrderRequest.PaymentMethodId);
            if (paymentMethod == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Payment Method ID " + createOrderRequest.PaymentMethodId + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var newOrder = new Order
            {
                UserId = createOrderRequest.UserId,
                TotalAmount = createOrderRequest.TotalAmount,
                Address = createOrderRequest.Address,
                Status = (int)OrderStatusEnum.New,
                CreateAt = DateTime.UtcNow,
                PaymentMethodId = createOrderRequest.PaymentMethodId,
                OrderDetails = createOrderRequest.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Amount = od.Amount,
                    Quantity = od.Quantity
                }).ToList()
            };

            _unitOfWork.OrderRepository.Insert(newOrder);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Create new order successfully",
                StatusCode = StatusCodes.Status201Created,
                Result = new OrderResponse
                {
                    Id = newOrder.Id,
                    UserId = newOrder.UserId,
                    TotalAmount = newOrder.TotalAmount ?? 0,
                    Address = newOrder.Address,
                    Status = newOrder.Status ?? 0,
                    CreateAt = newOrder.CreateAt ?? DateTime.MinValue,
                    PaymentMethodId = newOrder.PaymentMethodId ?? 0,
                    OrderDetails = newOrder.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Amount = od.Amount ?? 0,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                }
            };
            return response;
        }

        public async Task<BasicResponse> UpdateOrderAsync(UpdateOrderRequest updateOrderRequest)
        {
            var order = _unitOfWork.OrderRepository.GetByID(updateOrderRequest.Id);
            if (order == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Order ID " + updateOrderRequest.Id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var user = _unitOfWork.UserRepository.GetByID(updateOrderRequest.UserId);
            if (user == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "User ID " + updateOrderRequest.UserId + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var paymentMethod = _unitOfWork.PaymentMethodRepository.GetByID(updateOrderRequest.PaymentMethodId);
            if (paymentMethod == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Payment Method ID " + updateOrderRequest.PaymentMethodId + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            order.UserId = updateOrderRequest.UserId;
            order.TotalAmount = updateOrderRequest.TotalAmount;
            order.Address = updateOrderRequest.Address;
            order.PaymentMethodId = updateOrderRequest.PaymentMethodId;

            var existingOrderDetails = order.OrderDetails.ToList();
            foreach (var od in updateOrderRequest.OrderDetails)
            {
                var existingDetail = existingOrderDetails.FirstOrDefault(d => d.Id == od.Id);
                if (existingDetail != null)
                {
                    existingDetail.ProductId = od.ProductId;
                    existingDetail.Amount = od.Amount;
                    existingDetail.Quantity = od.Quantity;
                }
                else
                {
                    var newDetail = new OrderDetail
                    {
                        ProductId = od.ProductId,
                        Amount = od.Amount,
                        Quantity = od.Quantity,
                        OrderId = order.Id
                    };
                    _unitOfWork.OrderDetailRepository.Insert(newDetail);
                }
            }

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update order successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new OrderResponse
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TotalAmount = order.TotalAmount ?? 0,
                    Address = order.Address,
                    Status = order.Status ?? 0,
                    CreateAt = order.CreateAt ?? DateTime.MinValue,
                    PaymentMethodId = order.PaymentMethodId ?? 0,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Amount = od.Amount ?? 0,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                }
            };
            return response;
        }

        public async Task<BasicResponse> UpdateOrderStatusAsync(UpdateOrderStatusRequest updateOrderStatusRequest)
        {
            var order = _unitOfWork.OrderRepository.GetByID(updateOrderStatusRequest.Id);
            if (order == null)
            {
                return new BasicResponse
                {
                    IsSuccess = false,
                    Message = "Order ID " + updateOrderStatusRequest.Id + " does not exist",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            order.Status = (int)updateOrderStatusRequest.Status;

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveAsync();

            var response = new BasicResponse
            {
                IsSuccess = true,
                Message = "Update order status successfully",
                StatusCode = StatusCodes.Status200OK,
                Result = new OrderResponse
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TotalAmount = order.TotalAmount ?? 0,
                    Address = order.Address,
                    Status = order.Status ?? 0,
                    CreateAt = order.CreateAt ?? DateTime.MinValue,
                    PaymentMethodId = order.PaymentMethodId ?? 0,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Amount = od.Amount ?? 0,
                        Quantity = od.Quantity ?? 0
                    }).ToList()
                }
            };
            return response;
        }
    }
}
