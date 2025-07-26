namespace Catalog.API.Products.GetProductByCatagory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
                async (string category, IDocumentSession session, ILogger<GetProductByCategoryEndpoint> logger) =>
            {
                logger.LogInformation("Handling GetProductByCategory for CategoryId: {CategoryId}", category);

                var products = await session.Query<Product>()
                    .Where(p => p.Category.Contains(category)).ToListAsync();

                return Results.Ok(new GetProductByCategoryResponse(products));
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get Products by Category")
            .WithDescription("This endpoint retrieves products from the catalog by category.");
        }
    }
}
