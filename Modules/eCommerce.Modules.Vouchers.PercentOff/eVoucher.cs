using eCommerce.Contracts.Modules;
using eCommerce.Contracts.Modules.Vouchers.MoneyOff;

namespace eCommerce.Modules.Vouchers.PercentOff
{
    public class eVoucher : IeVoucher
    {
        public void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher)
        {
            decimal basketTotal = basket.BasketTotal();
            if (voucher.MinSpend < basketTotal)
            {
                basketVoucher.Value = voucher.Value * (basketTotal / 100) - 1;
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;
                basket.AddBasketVoucher(basketVoucher);
            }
        }
    }
}
