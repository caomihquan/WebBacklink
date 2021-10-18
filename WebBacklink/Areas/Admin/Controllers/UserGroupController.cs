using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        // GET: Admin/UserGroup
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            var dao = new GroupUserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var usergroup = new GroupUserDao().ViewDetail(id);
            return View(usergroup);
        }

        [HttpPost]
        public ActionResult Create(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new GroupUserDao();
                string id = dao.Insert(usergroup);
                if (id !=null)
                {
                    //SetAlert("Thêm Thành Công ", "Success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Edit(UserGroup usergroup)
        {
            if (ModelState.IsValid)
            {
                var dao = new GroupUserDao();
                var result = dao.Update(usergroup);
                if (result)
                {
                    //SetAlert("Thêm Thành Công ", "Success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            new GroupUserDao().Delete(id);
            return RedirectToAction("Index", "UserGroup");
        }
    }
}