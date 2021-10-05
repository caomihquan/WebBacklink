using Common;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebBacklink.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }



        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string contents)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Email = email;
            feedback.CreatedDate = DateTime.Now;
            feedback.Phone = mobile;
            feedback.Content = contents;
            feedback.Address = address;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
                
                //string a = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/Template/NewFeedback.html"));
                //a = a.Replace("{{CustomerName}}",name);
                //a = a.Replace("{{Phone}}", mobile);
                //a = a.Replace("{{Email}}", email);
                //a = a.Replace("{{Address}}", address);
                //a = a.Replace("{{Total}}", contents);
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Feedback Mới", a);
                //new MailHelper().SendMail(toEmail, "Feedback Mới", a);
            }

            else
                return Json(new
                {
                    status = false
                });



        }
        public ActionResult Success()
        {
            return View();
        }
    }
}