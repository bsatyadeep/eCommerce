using System.Linq;
using System.Web.Mvc;
using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using eCommmerce.Services;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryBase<Customer> _customers;
        private readonly IRepositoryBase<Product> _products;
        private readonly IRepositoryBase<Basket> _baskets;
        private readonly IRepositoryBase<BasketItem> _basketItems;
        private readonly IRepositoryBase<BasketVoucher> _basketVouchers;
        private readonly IRepositoryBase<VoucherType> _voucherTypes;
        private readonly IRepositoryBase<Voucher> _vouchers;
        private readonly BasketService basketService;

        public HomeController(IRepositoryBase<Customer> cuctomers, IRepositoryBase<Product> products, IRepositoryBase<Basket> baskets, IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<Voucher> vouchers, IRepositoryBase<VoucherType> vouchertypes, IRepositoryBase<BasketItem> basketItems)
        {
            _customers = cuctomers;
            _products = products;
            _baskets = baskets;
            _basketVouchers = basketVouchers;
            _voucherTypes = vouchertypes;
            _vouchers = vouchers;
            _basketItems = basketItems;
            basketService = new BasketService(_baskets, _basketVouchers, _vouchers, _voucherTypes, _basketItems);
        }

        public ActionResult Index()
        {
            var products = _products.GetAll();
            return View(products);
        }
        public ActionResult ProductDetails(int id)
        {
            Product product = _products.GetById(id);
            return View(product);
        }
        public ActionResult AddToBasket(int id)
        {
            basketService.AddToBasket(HttpContext, id, 1);  //Always add one to the Basket            
            return RedirectToAction("BasketSummary");
        }
        public ActionResult DeleteFromBasket(int id)
        {
            basketService.RemoveFromBasket(HttpContext, id, 1);  //Always remove one to the Basket            
            return RedirectToAction("BasketSummary");
        }
        public ActionResult BasketSummary()
        {
            var baskets = basketService.GetBasket(HttpContext);
            return View(baskets);
        }
        public ActionResult AddBasketVoucher(string voucherCode)
        {
            basketService.AddVoucher(voucherCode, HttpContext);
            return RedirectToAction("BasketSummary");
        }

        public ActionResult DeleteBasketVoucher(int id)
        {
            basketService.DeleteVoucher(id, HttpContext);
            return RedirectToAction("BasketSummary");
        }
    }
}