using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Store.Catalogue.Domain.Product;

namespace Store.Catalogue.Application.Product.Command.Create
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        
        public async Task<Unit> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _productRepository.CreateProductAsync(Domain.Product.Product.Create(
                Guid.NewGuid(),
                request.Name, 
                request.Price, 
                request.Description));

            return Unit.Value;
        }
    }
}