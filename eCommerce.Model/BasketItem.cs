using System;
using eCommerce.Contracts.Modules;

namespace eCommerce.Model
{
    public class BasketItem : IBasketItem
    {
        private Product _product;
        public int BasketItemId { get; set; }
        public Guid BasketId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual IProduct IProduct { get { return _product as IProduct; } set { _product = value as Product; } }
        public virtual Product Product { get { return _product; } set { _product = value; } }
    }
}
