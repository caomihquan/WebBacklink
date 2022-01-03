using BotDetect.Web.Mvc;
using Facebook;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBacklink.Common;
using WebBacklink.Models;

namespace WebBacklink.Controllers
{
    public class UserController : Controller
    {

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
            [AllowAnonymous]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = /*ConfigurationManager.AppSettings["FbAppId"]*/1024793591616473,
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = /*ConfigurationManager.AppSettings["FbAppId"]*/1024793591616473,
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName = firstname + " " + middlename + " " + lastname;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }

            return Redirect("/");
        }
        public ActionResult Profilee()
        {
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return View("Index");
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;

            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new User();
                    userSession.UserName = user.UserName;
                    userSession.ID = user.ID;
                    userSession.Password = user.Password;
                    userSession.Name = user.Name;
                    userSession.Phone = user.Phone;
                    userSession.Email = user.Email;
                    userSession.Address = user.Address;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản Không Tồn Tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Đang Bị Khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu Không Đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }

            return View(model);
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.ConfirmPassword = Encryptor.MD5Hash(model.ConfirmPassword);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Name = model.Name;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                        return Redirect("/dang-nhap");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công.");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                
                if (!string.IsNullOrEmpty(user.Password)&& !string.IsNullOrEmpty(user.ConfirmPassword) && !string.IsNullOrEmpty(user.CreatedBy))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    var encryptedMd5Pass = Encryptor.MD5Hash(user.ConfirmPassword);
                    var encryptedMd5Passold = Encryptor.MD5Hash(user.CreatedBy);
                    user.Password = encryptedMd5Pas;
                    user.ConfirmPassword = encryptedMd5Pass;
                    user.CreatedBy = encryptedMd5Passold;
                }

                if (dao.CheckPassword(user.CreatedBy))
                    {
                    var result = dao.Update(user);
                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật user Không thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật Khẩu Cũ Không Đúng");
                }
                
                
            }
            return View(user);
        }
    }
}