namespace Catalog.API.Products.CreateProduct
{
    public record CreatreProductRequest(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price
    );

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreatreProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Create a new product")
                .WithDescription("This endpoint allows you to create a new product in the catalog.");
        }
    }
}
