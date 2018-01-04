using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;

namespace CmsSystem.Service
{
    public interface IProductService
    {
        
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
