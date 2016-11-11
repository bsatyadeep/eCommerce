using System;
using eCommerce.DataAccess.Data;
using eCommerce.Model;

namespace eCommerce.DataAccess.Repositories
{
    public class VoucherTypeRepository : RepositoryBase<VoucherType>
    {
        public VoucherTypeRepository(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
