namespace eCommerce.Contracts.Modules.Vouchers.MoneyOff
{
    public interface IeVoucher
    {
        void ProcessVoucher(IVoucher voucher, IBasket basket, IBasketVoucher basketVoucher);
    }
}