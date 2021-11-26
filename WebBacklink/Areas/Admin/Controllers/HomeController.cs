using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View();
        }
    }
}