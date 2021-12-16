using Models.Dao;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBacklink.Common;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
       
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.ViewDetail(id);
            SetViewBag(content.CategoryID);

            return View(content);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content model)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật Không thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if(ModelState.IsValid)
            {
                SetAlert("Thêm Thành Công ", "success");
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.CreatedBy = session.UserName;
                //var culture = Session[CommonConstants.]
                //content.Language = culture.ToString();
                new ContentDao().Create(content);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View("Index");
        }
        public void SetViewBag(long? selectedId=null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = new ContentDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "Content");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Không thành công");
            }
            return View("Index");

        }
    }
}