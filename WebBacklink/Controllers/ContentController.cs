using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index(string searchString,int page=1,int pageSize=3)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Detail(long id)
        {

            var content = new ContentDao().ViewDetail(id);
            ViewBag.Content = new CategoryDao().ViewDetail(content.CategoryID.Value);
            return View(content);
        }
    }
}