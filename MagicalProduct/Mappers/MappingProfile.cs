using AutoMapper;
using MagicalProduct.API.Models;
using MagicalProduct.API.Payload.Request.Card;
using MagicalProduct.API.Payload.Response.Card;
using MagicalProduct.API.Payload.Response.Payment;
using System.Net;

namespace MagicalProduct.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaymentMethod, PaymentResponse>();
            CreateMap<Card, CardResponse>()
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentMethod.PaymentType))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<CardReq, Card>();
        }
    }
}
