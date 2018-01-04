using CmsSystem.Data.Infrastructure;
using CmsSystem.Data.Repositories;

namespace CmsSystem.Service
{
    public interface IProductCategoryService
    {

    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
