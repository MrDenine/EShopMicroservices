﻿
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            logger.LogInformation("GetProductsQuery,Handle called with {@Query}", query);

            var product = await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(product);
        }
    }
}
