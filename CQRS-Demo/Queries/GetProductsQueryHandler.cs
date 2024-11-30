using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRS_Demo.Data;
using CQRS_Demo.Models;
using Microsoft.EntityFrameworkCore;
using CQRS_Demo.Queries;

namespace CQRS_Demo.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly ApplicationDbContext _context;

        public GetProductsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}