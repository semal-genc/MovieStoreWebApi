using AutoMapper;
using MovieStore.Application.Commands.Customer.CreateCustomer;
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
        }
    }
}
