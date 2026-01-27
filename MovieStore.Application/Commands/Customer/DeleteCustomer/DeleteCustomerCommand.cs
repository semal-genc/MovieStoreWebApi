using MediatR;

namespace MovieStore.Application.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int CustomerId { get; }

        public DeleteCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}