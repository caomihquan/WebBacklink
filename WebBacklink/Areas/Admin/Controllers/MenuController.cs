using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            var dao = new MenuDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var menu = new MenuDao().ViewDetail(id);
            return View(menu);
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                long id = dao.Insert(menu);
                if (id > 0)
                {
                    SetAlert("Thêm Thành Công ", "Success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuDao();
                var result = dao.Update(menu);
                if (result)
                {
                    SetAlert("Thêm Thành Công ", "Success");
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDao().Delete(id);
            return RedirectToAction("Index", "Menu");
        }
    }
}