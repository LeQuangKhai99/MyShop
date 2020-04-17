using Model.Dao;
using Model.EF;
using MyShop.Common;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyShop.Controllers
{
    public class LoginController : Controller
    {
       
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckIssetUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckIssetEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Phone = model.Phone;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    user.Email = model.Email;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    dao.Insert(user);
                    ViewBag.Success = "Đăng kí thành công"; 
                }
            }
            else
            {
                return View();
            }
            return Redirect("/dang-nhap");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                
                int data = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                switch (data)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản ko tồn tại!");
                        break;
                    case -2:
                        ModelState.AddModelError("", "Sai mật khẩu!");
                        break;
                    case -1:
                        ModelState.AddModelError("", "Tài khoản bị khóa!");
                        break;
                    case 1:
                        var user = new UserLogin();
                        user.UserName = model.UserName;
                        Session.Add(Constants.USER_SESSION, user);
                        return Redirect("/");
                        break;
                }
            }
   
            return View();

           
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session[Common.Constants.USER_SESSION] = null;
            return Redirect("/");
        }

    }
}