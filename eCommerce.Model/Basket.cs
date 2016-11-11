using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using eCommerce.Contracts.Modules;

namespace eCommerce.Model
{
    public class Basket : IBasket
    {
        [Key]
        public Guid BasketId { get; set; }
        public DateTime BasketDate { get; set; }
        private List<BasketItem> basketItems;
        private List<BasketVoucher> basketVouchers;

        public virtual ICollection<IBasketItem> IBasketItems { get { return basketItems.ConvertAll(i => (IBasketItem)i); } }
        public virtual ICollection<BasketItem> BasketItems { get { return basketItems; } set { basketItems = value.ToList(); } }

        public virtual ICollection<IBasketVoucher> IBasketVouchers { get { return basketVouchers.ConvertAll(i => (IBasketVoucher)i); } }
        public virtual ICollection<BasketVoucher> BasketVouchers { get { return basketVouchers; } set { basketVouchers = value.ToList(); } }

        public Basket()
        {
            basketItems = new List<BasketItem>();
            basketVouchers = new List<BasketVoucher>();
        }
        public decimal BasketTotal()
        {
            decimal basketTotal = 0;
            if (BasketItems != null)
            {
                basketTotal += BasketItems.Sum(basketItem => basketItem.Quantity * basketItem.Product.Price);
            }
            return basketTotal;
        }

        public decimal BasketItemCount()
        {
            return BasketItems.Count;
        }
        public void AddBasketItem(IBasketItem item)
        {
            basketItems.Add((BasketItem)item);
        }

        public void DeleteBasketItem(IBasketItem item)
        {
            basketItems.Remove((BasketItem)item);
        }

        public void AddBasketVoucher(IBasketVoucher voucher)
        {
            basketVouchers.Add((BasketVoucher)voucher);
        }
    }
}
