using System;
using eCommerce.DataAccess.Data;
using eCommerce.Model;

namespace eCommerce.DataAccess.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
