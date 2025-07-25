﻿
namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductCommandHandler(
        IDocumentSession session, ILogger<DeleteProductCommandHandler> logger
        ) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeleteProductCommand for product with ID: {Id}", request.Id);

            session.Delete<Product>(request.Id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
