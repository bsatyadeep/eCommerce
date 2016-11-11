using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System.Web.Mvc;

namespace eCommerce.Web.Controllers
{
    public class AdminController : Controller
    {
        private IRepositoryBase<Customer> _customers;
        private readonly IRepositoryBase<Product> _products;
        private readonly IRepositoryBase<VoucherType> _voucherTypes;
        private readonly IRepositoryBase<Voucher> _vouchers;
        public AdminController(IRepositoryBase<Customer> cuctomers, IRepositoryBase<Product> products, IRepositoryBase<Voucher> vouchers, IRepositoryBase<VoucherType> vouchertypes)
        {
            _customers = cuctomers;
            _products = products;
            _vouchers = vouchers;
            _voucherTypes = vouchertypes;
        }
        // GET: Admin
        public ActionResult Index()
        {
            var products = _products.GetAll();
            return View(products);
        }
        public ActionResult CreateProduct()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _products.Insert(product);
                _products.Commit();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditProduct(int id)
        {
            Product product = _products.GetById(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _products.Update(product);
                _products.Commit();
            }
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                _products.Delete(id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateVoucherType()
        {
            return View(new VoucherType());
        }
        [HttpPost]
        public ActionResult CreateVoucherType(VoucherType voucherType)
        {
            if (ModelState.IsValid)
            {
                _voucherTypes.Insert(voucherType);
                _voucherTypes.Commit();
                return RedirectToAction("VoucherTypeSummary");
            }
            return View(voucherType);
        }
        [HttpDelete]
        public ActionResult DeleteVoucherType(int id)
        {
            if (ModelState.IsValid)
                _voucherTypes.Delete(id);
            return RedirectToAction("VoucherTypeSummary");
        }
        public ActionResult EditVoucherType(int id)
        {
            VoucherType voucherType = _voucherTypes.GetById(id);
            return View(voucherType);
        }
        [HttpPost]
        public ActionResult EditVoucherType(VoucherType voucherType)
        {
            if (ModelState.IsValid)
            {
                _voucherTypes.Update(voucherType);
                _voucherTypes.Commit();
                return RedirectToAction("VoucherTypeSummary");
            }
            return View(voucherType);
        }
        public ActionResult VoucherTypeSummary()
        {
            var voucherTypes = _voucherTypes.GetAll();
            return View(voucherTypes);
        }

        public ActionResult CreateVoucher()
        {
            ViewBag.VoucherTypes = _voucherTypes.GetAll();
            ViewBag.Products = _products.GetAll();
            return View(new Voucher());
        }
        [HttpPost]
        public ActionResult CreateVoucher(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                _vouchers.Insert(voucher);
                _vouchers.Commit();
                return RedirectToAction("VoucherSummary");
            }
            return View(voucher);
        }
        public ActionResult VoucherSummary()
        {
            var vouchers = _vouchers.GetAll();
            return View(vouchers);
        }
        public ActionResult EditVoucher(int id)
        {
            Voucher voucher = _vouchers.GetById(id);
            return View(voucher);
        }
        [HttpDelete]
        public ActionResult DeleteVoucher(int id)
        {
            if (ModelState.IsValid)
            {
                var voucher = _vouchers.GetAll(id);
                if (voucher != null)
                    _vouchers.Delete(voucher);
            }
            return RedirectToAction("VoucherSummary");
        }
        [HttpPost]
        public ActionResult EditVoucher(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                _vouchers.Update(voucher);
                _vouchers.Commit();
                return RedirectToAction("VoucherSummary");
            }
            return View(voucher);
        }
    }
}