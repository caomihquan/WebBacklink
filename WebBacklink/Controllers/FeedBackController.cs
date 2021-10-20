using BotDetect.Web.Mvc;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBacklink.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "feedbackCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                var dao = new FeedBackDao();
                long id = dao.Insert(feedback);
                if (id > 0)
                {
                    ViewBag.Success = "Đăng Kí Thành Công";
                    return RedirectToAction("Create", "FeedBack");
                }
                else
                {
                    ModelState.AddModelError("", "Không Thành Công");
                    
                }
            }
            return View("Create");
        }
    }
}