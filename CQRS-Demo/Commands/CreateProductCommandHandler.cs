using MediatR;

namespace CQRS_Demo.Commands
{
    public class CreateProductCommandHandler : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}
