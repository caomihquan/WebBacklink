using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            var dao = new SlideDao();
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
            var slide = new SlideDao().ViewDetail(id);
            return View(slide);
        }

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();
                long id = dao.Insert(slide);
                if (id > 0)
                {
                    SetAlert("Thêm Thành Công ", "Success");
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm  Không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();
                var result = dao.Update(slide);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "Success");
                    return RedirectToAction("Index", "Slide");
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
            new SlideDao().Delete(id);
            return RedirectToAction("Index", "Slide");
        }
    }
}