using eCommerce.Contracts.Modules;
using eCommerce.Contracts.Modules.Vouchers.MoneyOff;

namespace eCommerce.Modules.Vouchers.MoneyOff
{
    public class eVoucher : IeVoucher
    {
        public void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher)
        {
            if (voucher.MinSpend < basket.BasketTotal())
            {
                basketVoucher.Value = voucher.Value;
                basketVoucher.VoucherCode = voucher.VoucherCode;
                basketVoucher.VoucherDescription = voucher.VoucherDescription;
                basketVoucher.VoucherId = voucher.VoucherId;
                basket.AddBasketVoucher(basketVoucher);
            }
        }
    }
}