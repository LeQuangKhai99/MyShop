using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.slide = new SlideDao().GetSlide();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(4);
            ViewBag.FeatureProducts = productDao.ListFeatureProduct(4);
            return View();
        }

        // ko gọi trức tiếp thành trang đc
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        public ActionResult MenuTop()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}