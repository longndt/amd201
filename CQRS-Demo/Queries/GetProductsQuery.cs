using MediatR;
using CQRS_Demo.Models;
using System.Collections.Generic;

namespace CQRS_Demo.Queries
{
    public class GetProductsQuery : IRequest<List<Product>> { }
}
