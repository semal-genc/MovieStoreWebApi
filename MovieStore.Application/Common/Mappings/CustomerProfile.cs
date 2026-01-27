using AutoMapper;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;
using MovieStore.Application.Features.Customers.Dtos;
using MovieStore.Domain.Entities;

namespace MovieStore.Application.Common.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<FavoriteGenreDto, CustomerFavoriteGenre>();
        }
    }
}
