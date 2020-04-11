using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().GetProductCategory();
            return PartialView(model);
        }

        public ActionResult Category(long cateId = 99)
        {
            var cate = new CategoryDao().ViewDetail(cateId);
            return View(cate);
        }

        public ActionResult Detail(long productId = 1)
        {
            var product = new ProductDao().GetProductById(productId);
            return View(product);
        }
    }
}