
using AutoMapper;
using MovieStore.Application.Features.Orders.Commands.BuyMovie;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<BuyMovieCommand, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.PurchaseDate, opt => opt.Ignore());
        }
    }
}