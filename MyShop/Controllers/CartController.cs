using Model.Dao;
using Model.EF;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace MyShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var Product = new ProductDao().GetProductById(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                // Nếu trong danh sách đã tồn tại sản phẩm thì tăng số lượng
                if(list.Exists(x=>x.product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.product = Product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
                
            }
            else
            {
                // Tạo mới đối tượng
                var item = new CartItem();
                item.product = Product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                // gán vào session
                Session[CartSession] = list;
            }
            TempData["Cart"] = "Thêm sản phẩm vào giỏ hàng thành công!";
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult UpDate(FormCollection f)
        {
            // lấy tất cả các value của input có name là quantity
            string[] quantity = f.GetValues("quantity");
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                for (int i = 0; i < list.Count; i++)
                {
                    int num = Convert.ToInt32(quantity[i]);
                    if (num < 1) num = 1;
                    if (num > 10) num = 10;
                    list[i].Quantity = num;
                }
                Session[CartSession] = list;
            }
            TempData["Cart"] = "Cập nhật giỏ hàng thành công!";
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Delete(int idProduct)
        {
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                var temp = new List<CartItem>();
                int check = 0;
                int i = 0;

                list = list.Where(item => item.product.ID != idProduct).ToList();
                Session[CartSession] = list;
                TempData["Cart"] = "Xóa Sản Phẩm Thành Công";
            }
            
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public ActionResult Pay()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Pay(string name,string phone, string address, string email)
        {
            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipName = name;
            order.ShipPhone = phone;
            order.ShipEmail = email;

            var orderDao = new OrderDao();
            var orderDetaiDao = new OrderDetailDao();

            long id = orderDao.Insert(order);

            var cart = (List<CartItem>)Session[CartSession];
            decimal total = 0;
            int price = 0;
            foreach (var item in cart)
            {
                var orderDetail = new OrderDetail();
                orderDetail.ProductID = item.product.ID;
                orderDetail.OrderID = id;
                orderDetail.Price = item.product.Price;
                orderDetail.Quantity = item.product.Quantity;
                orderDetaiDao.Insert(orderDetail);

                if (item.product.PromotionPrice != null)
                {
                    price = (int)item.product.PromotionPrice;
                }
                else
                {
                    price = (int)item.product.Price;
                }
                total += price * item.Quantity;
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

            content = content.Replace("{{CustomerName}}", name);
            content = content.Replace("{{Phone}}", phone);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", total.ToString("N0"));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Đơn hàng mới từ MyShop", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ MyShop", content);
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }

    
}