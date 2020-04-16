using Model.Dao;
using Model.EF;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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

        // ko gọi trức tiếp thành trang đc
        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[Common.Constants.CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string phone, string email, string address, string content)
        {
            FeedBack fb = new FeedBack();
            fb.Name = name;
            fb.Phone = phone;
            fb.Email = email;
            fb.Address = address;
            fb.Content = content;
            fb.CreatedDate = DateTime.Now;
            fb.Status = true;

            FeedBackDao fbd = new FeedBackDao();
            fbd.Insert(fb);
            
            return Redirect("/");
        }
    }
}