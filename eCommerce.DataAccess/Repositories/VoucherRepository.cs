using System;
using eCommerce.DataAccess.Data;
using eCommerce.Model;

namespace eCommerce.DataAccess.Repositories
{
    public class VoucherRepository : RepositoryBase<Voucher>
    {
        public VoucherRepository(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
