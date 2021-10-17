
using Models.Dao;
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

        [ChildActionOnly]
        public PartialViewResult Category()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Detail(long id)
        {

            var model = new ContentDao().GetByID(id);
            ViewBag.RelatedContents = new ContentDao().ListRelatedContents(id,1);
            ViewBag.Tags = new ContentDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAllByTag(tagId, page, pageSize);
            int totalRecord = 0;

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            ViewBag.Tag = new ContentDao().GetTag(tagId);
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        public ActionResult MenuCategory(long id, int page = 1, int pageSize = 10)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ContentDao().ListByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }

    }
}