using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Admin/Role
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            var dao = new RoleDao();
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
            var role = new RoleDao().ViewDetail(id);
            return View(role);
        }

        [HttpPost]
        
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDao();
                string id = dao.Insert(role);
                if (id != null)
                {
                    SetAlert("Thêm Thành Công ", "success");
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không Thành Công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDao();
                var result = dao.Update(role);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var result = new RoleDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }
    }
}