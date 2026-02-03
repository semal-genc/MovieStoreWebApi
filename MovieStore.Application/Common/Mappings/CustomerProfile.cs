using AutoMapper;
using MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteGenres, opt => opt.Ignore());

            CreateMap<AddFavoriteGenreCommand, CustomerFavoriteGenre>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

        }
    }
}
