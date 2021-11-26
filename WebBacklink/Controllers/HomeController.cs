using Models.Dao;
using Models.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBacklink.Common;
using WebBacklink.Models;

namespace WebBacklink.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(8);
            ViewBag.NewContents = new ContentDao().ListNewContent(3);
            ViewBag.ListFeatureProducts = productDao.ListFeatureProduct(6);

            //seo title
            ViewBag.Title = ConfigurationManager.AppSettings["HomeTitle"];
            ViewBag.Keywords = ConfigurationManager.AppSettings["HomeKeyWord"];
            ViewBag.Descriptions = ConfigurationManager.AppSettings["HomeDescriptions"];

            return View();
        }
        [ChildActionOnly]
        //[OutputCache(Duration =3600*24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderSave()
        {
            var save = Session[CommonConstants.SaveSession];
            
            var list = new List<SaveItem>();
            
            if (save != null)
            {
                list = (List<SaveItem>)save;

            }
           
            return PartialView(list);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }


    }
}