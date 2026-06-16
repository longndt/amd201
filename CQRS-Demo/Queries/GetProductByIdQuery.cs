using MediatR;
using CQRS_Demo.Models;

namespace CQRS_Demo.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
