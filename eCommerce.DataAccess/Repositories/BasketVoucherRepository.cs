using System;
using eCommerce.DataAccess.Data;
using eCommerce.Model;

namespace eCommerce.DataAccess.Repositories
{
    public class BasketVoucherRepository : RepositoryBase<BasketVoucher>
    {
        public BasketVoucherRepository(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
