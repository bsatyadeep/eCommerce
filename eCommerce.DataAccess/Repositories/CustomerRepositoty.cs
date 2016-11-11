using System;
using eCommerce.DataAccess.Data;
using eCommerce.Model;

namespace eCommerce.DataAccess.Repositories
{
    public class CustomerRepositoty : RepositoryBase<Customer>
    {
        public CustomerRepositoty(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}