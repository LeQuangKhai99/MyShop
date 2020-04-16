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

        public ActionResult Category(long cateId = 99, int page = 1, int pageSize = 2)
        {
            
            if (page <= 0)
            {
                page = 1;
            }
            var cate = new CategoryDao().ViewDetail(cateId);
            ViewBag.cate = cate.Name;
            string url = "/san-pham/" + cate.MetaTitle + "-" + cate.ID;
            ViewBag.url = url;
            int total = 0;
            ViewBag.page = page;
            var product = new ProductDao().GetListProductsByCategoryId(cateId, ref total, page, pageSize);
            ViewBag.totalPage = (int)Math.Ceiling(((double)total / (double)pageSize));
            ViewBag.total = total;
            return View(product);
        }

        public ActionResult Detail(long productId = 1)
        {
            var product = new ProductDao().GetProductById(productId);
            ViewBag.category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.relate = new ProductDao().ListRelateProduct(product.ID, 4);
            return View(product);
        }
    }
}