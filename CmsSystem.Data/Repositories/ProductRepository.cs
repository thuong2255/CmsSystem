using CmsSystem.Data.Infrastructure;
using CmsSystem.Model.Models;

namespace CmsSystem.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
