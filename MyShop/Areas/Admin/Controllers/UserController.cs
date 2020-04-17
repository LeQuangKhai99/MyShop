using Model.Dao;
using Model.EF;
using MyShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.Admin.Controllers
{
    public class UserController : BaseController

    {
        // GET: Admin/User
        public ActionResult Index(string search, int page = 1, int pageSize = 1)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(search, page, pageSize);
            ViewBag.search = search;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var isset = dao.CheckIssetUserName(user.UserName);
                if(isset)
                {
                    var check = dao.CheckInsert(user);
                    switch (check)
                    {
                        case 1:
                            ModelState.AddModelError("", "Mời nhập tên!");
                            break;
                        case 2:
                            ModelState.AddModelError("", "Mời nhập địa chỉ!");
                            break;
                        case 3:
                            ModelState.AddModelError("", "Mời nhập số điện thoại!");
                            break;
                        case 4:
                            ModelState.AddModelError("", "Mời nhập tên tài khoản!");
                            break;
                        case 5:
                            ModelState.AddModelError("", "Mời nhập mật khẩu!");
                            break;
                        case 6:
                            ModelState.AddModelError("", "Mời nhập email!");
                            break;
                        case 0:
                            user.Password = Encryptor.MD5Hash(user.Password);
                            long id = dao.Insert(user);
                            if (id > 0)
                            {
                                // chuyển trang user/index
                                SetAlert("Tạo User Thành Công", "success");
                                return RedirectToAction("Index", "User");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Thêm user không thành công!");

                            }
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name này đã được đăng kí!");
                }
            }
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDao().GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var check = dao.CheckUpdate(user);

                switch (check)
                {
                    case 1:
                        ModelState.AddModelError("", "Mời nhập tên!");
                        break;
                    case 2:
                        ModelState.AddModelError("", "Mời nhập địa chỉ!");
                        break;
                    case 3:
                        ModelState.AddModelError("", "Mời nhập số điện thoại!");
                        break;
                    case 0:
                        var result = dao.Update(user);
                        if (result)
                        {
                            // chuyển trang user/index
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật user không thành công!");

                        }
                        break;
                }

            }
            return View("Edit");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}