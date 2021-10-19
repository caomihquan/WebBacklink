using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            var dao = new FooterDao();
            var model = dao.ListAll();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var footer = new FooterDao().GetByID(id);
            return View(footer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Footer footer)
        {
            if (ModelState.IsValid)
            {
                var dao = new FooterDao();
                var result = dao.Update(footer);
                if (result)
                {
                    SetAlert("Sửa Thành Công ", "success");
                    return RedirectToAction("Index", "Footer");
                }
                else
                {
                    ModelState.AddModelError("", "Update Không thành công");
                }
            }
            return View("Index");
        }
    }
}