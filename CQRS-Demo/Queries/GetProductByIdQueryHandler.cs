using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CQRS_Demo.Data;
using CQRS_Demo.Models;
using CQRS_Demo.Queries;

namespace CQRS_Demo.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly ApplicationDbContext _context;

        public GetProductByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(request.Id);
        }
    }
}

