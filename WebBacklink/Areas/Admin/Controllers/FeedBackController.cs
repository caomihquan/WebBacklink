using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class FeedBackController : BaseController
    {
        // GET: Admin/FeedBack
        
        public ActionResult Index(int page=1,int pageSize=5)
        {
            var dao = new FeedBackDao();
            var model = dao.ListAllPaging(page, pageSize);
            
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new FeedBackDao().Delete(id);
            return RedirectToAction("Index", "FeedBack");
        }
    }
}