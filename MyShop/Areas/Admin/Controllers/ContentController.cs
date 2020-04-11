using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);
            if(content == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SetViewBag(content.CategoryID);
            return View(content);
        }

        public void SetViewBag(long? selected = null)
        {
            var dao = new CategoryDao();
            // lấy ra danh sách các cate để chuyển sang view
            // tham số 1: danh sách value
            // tham số 2: giá trị của value
            // tham số 3: giá trị đc hiển thị lên
            // tham số 4: giá trị đc chọn
            // Lưu ý phải đặt giá trị của viewbag trùng với thuộc tính muốn hiển thị
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selected);
        }
    }
}