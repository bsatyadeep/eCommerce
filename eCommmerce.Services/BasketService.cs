using System;
using System.Linq;
using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System.Web;
using eCommerce.Contracts.Modules.Vouchers.MoneyOff;
using eCommerce.DataAccess.Data;
using eCommerce.DataAccess.Repositories;

namespace eCommmerce.Services
{
    public class BasketService
    {
        private readonly IRepositoryBase<Basket> _basbaskets;
        private readonly IRepositoryBase<BasketItem> _basbasketItems;
        private readonly IRepositoryBase<BasketVoucher> _basketVouchers;
        private readonly IRepositoryBase<Voucher> _vouchers;
        private readonly IRepositoryBase<VoucherType> _vouchertypes;
        public string BasketSessionName { get; set; }
        public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<Voucher> vouchers, IRepositoryBase<VoucherType> vouchertypes, IRepositoryBase<BasketItem> basketItems)
        {
            BasketSessionName = "eCommerceBasket";
            _basbaskets = baskets;
            _basbasketItems = basketItems;
            _basketVouchers = basketVouchers;
            _vouchers = vouchers;
            _vouchertypes = vouchertypes;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            //create new basket

            Basket basket = new Basket
            {
                BasketDate = DateTime.Now,
                BasketId = Guid.NewGuid()
            };
            //add and persist in database
            _basbaskets.Insert(basket);
            _basbaskets.Commit();

            //create a new cookie
            //add basket id to cookie
            HttpCookie cookie = new HttpCookie(BasketSessionName)
            {
                Value = basket.BasketId.ToString(),
                Expires = DateTime.Now.AddDays(1)
            };
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }

        public bool RemoveFromBasket(HttpContextBase httpContext, int productId, int productQuantity)
        {
            bool isRemoved = false;
            Basket basket = GetBasket(httpContext);
            BasketItem item = basket.BasketItems?.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (item.Quantity >= productQuantity)
                {
                    item.Quantity = item.Quantity - productQuantity;
                    _basbaskets.Update(basket);
                    _basbaskets.Commit();
                }
                else
                {
                    _basbasketItems.Delete(item);
                    _basbasketItems.Commit();
                    //_basbaskets.Delete(basket);
                }
                isRemoved = true;
            }
            return isRemoved;
        }
        public bool AddToBasket(HttpContextBase httpContext, int productId, int productQuantity)
        {
            bool isAdded = false;
            Basket basket = GetBasket(httpContext);
            BasketItem item = basket.BasketItems?.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem
                {
                    BasketId = basket.BasketId,
                    ProductId = productId,
                    Quantity = productQuantity
                };
                basket.BasketItems?.Add(item);
                isAdded = true;
            }
            else
            {
                item.Quantity = item.Quantity + productQuantity;
            }
            _basbaskets.Commit();
            return isAdded;
        }

        public Basket GetBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            if (cookie != null)
            {
                Guid basketId;
                basket = Guid.TryParse(cookie.Value, out basketId) ? _basbaskets.GetById(basketId) : CreateNewBasket(httpContext);
            }
            else
            {
                basket = CreateNewBasket(httpContext);
            }
            return basket;
        }

        public void AddVoucher(string voucherCode, HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext);
            Voucher voucher = _vouchers.GetAll().FirstOrDefault(v => v.VoucherCode == voucherCode);
            if (voucher != null)
            {
                VoucherType voucherType = _vouchertypes.GetById(voucher.VoucherTypeId);
                if (voucherType != null)
                {
                    BasketVoucher basketVoucher = new BasketVoucher();
                    //implements without modules
                    //if (voucherType.Type == "MoneyOff")
                    //{
                    //    MoneyOff(voucher, basket, basketVoucher);
                    //}
                    //if (voucherType.Type == "PercentOff")
                    //{
                    //    PercentOff(voucher, basket, basketVoucher);
                    //}
                    //_basbaskets.Commit();
                    //implements modules
                    try
                    {
                        //Modules must be activated using full name of the class and containing project name e.g.
                        //<full name of class including namespace>,<full name of namespace> e.g
                        //eCommerce.Modules.Vouchers.MoneyOff.eVoucher, eCommerce.Modules.Vouchers.MoneyOff
                        //eCommerce.Modules.Vouchers.PercentOff.eVoucher, eCommerce.Modules.Vouchers.PercentOff
                        IeVoucher voucherProcessor =
                            Activator.CreateInstance(Type.GetType(voucherType.VoucherModule)) as IeVoucher;
                        voucherProcessor?.ProcessVoucher(voucher, basket, basketVoucher);
                        _basbaskets.Commit();
                    }
                    catch (Exception exception)
                    {
                        //log an error
                    }
                }
            }
        }
        public void DeleteVoucher(int basketVoucherId, HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext);
            BasketVoucher basketVoucher = basket.BasketVouchers.FirstOrDefault(i => i.BasketVoucherId == basketVoucherId);
            if (basketVoucher != null)
            {
                _basketVouchers.Delete(basketVoucher);
                _basketVouchers.Commit();
            }

        }
        ////without Module Implementation
        //private void MoneyOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        //{
        //    decimal basketTotal = basket.BasketTotal();
        //    if (voucher.MinSpend < basketTotal)
        //    {
        //        basketVoucher.Value = voucher.Value * -1;
        //        basketVoucher.VoucherCode = voucher.VoucherCode;
        //        basketVoucher.VoucherDescription = voucher.VoucherDescription;
        //        basketVoucher.VoucherId = voucher.VoucherId;
        //        //basket.AddBasketVoucher(basketVoucher);
        //        _basketVouchers.Insert(basketVoucher);
        //        _basketVouchers.Commit();
        //    }
        //}
        ////without Module Implementation
        //private void PercentOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        //{
        //    decimal basketTotal = basket.BasketTotal();
        //    if (voucher.MinSpend < basketTotal)
        //    {
        //        basketVoucher.Value = voucher.Value * (basketTotal / 100) - 1;
        //        basketVoucher.VoucherCode = voucher.VoucherCode;
        //        basketVoucher.VoucherDescription = voucher.VoucherDescription;
        //        basketVoucher.VoucherId = voucher.VoucherId;
        //        //basket.AddBasketVoucher(basketVoucher);
        //        _basketVouchers.Insert(basketVoucher);
        //        _basketVouchers.Commit();
        //    }
        //}
    }
}