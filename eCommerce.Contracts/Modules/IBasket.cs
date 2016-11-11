using System;
using System.Collections.Generic;

namespace eCommerce.Contracts.Modules
{
    public interface IBasket
    {
        Guid BasketId { get; set; }

        ICollection<IBasketItem> IBasketItems { get; }
        ICollection<IBasketVoucher> IBasketVouchers { get; }
        DateTime BasketDate { get; set; }

        void AddBasketItem(IBasketItem item);
        void AddBasketVoucher(IBasketVoucher voucher);
        decimal BasketTotal();
        decimal BasketItemCount();
        void DeleteBasketItem(IBasketItem item);
    }
}