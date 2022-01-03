using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBacklink.Models;
using WebBacklink.Common;
using Models.EF;

namespace WebBacklink.Controllers
{
    
    public class SaveController : BaseController
    {
        private const string SaveSession = "SaveSession";
        // GET: Save
        public ActionResult Index()
        {
           /* var save = Session[SaveSession];*/
            var session = (User)Session[CommonConstants.USER_SESSION];
            

/*            if (save != null)
            {
                var list = (List<SaveItem>)save;
                *//*var detaildao = new SaveDetailDao();
                foreach (var item in list)
                {
                    var orderdetail = new SaveDetail();
                    orderdetail.ProductID = item.Product.ID;
                    orderdetail.UserID = session.ID;
                    orderdetail.Image = item.Product.Image;
                    orderdetail.Name = item.Product.Name;
                    orderdetail.Metatitle = item.Product.MetaTitle;
                    orderdetail.Status = true;
                    detaildao.Insert(orderdetail);

                }*//*

            }   */        
            var listsave = new SaveDetailDao().List(session.ID);                     
            return View(listsave);

        }


/*        public JsonResult DeleteAll()
        {
            Session[SaveSession] = null;
            return Json(new
            {
                status = true
            }) ;
        }*/

        

        
        public ActionResult AddItem(long productId,int quantity,SaveDetail save)
        {
            var product = new ProductDao().ViewDetail(productId);
            var session = (User)Session[CommonConstants.USER_SESSION];
            /*var save = Session[SaveSession];*/
            var list = new List<SaveItem>();
            if (session != null )
            {
                var item = new SaveItem();
                var detaildao = new SaveDetailDao();
                var orderdetail = new SaveDetail();
                if (detaildao.Check(productId,session.ID))
                {
                    ModelState.AddModelError("", "Sản Phẩm đã tồn tại");
                }
                else
                {
                    item.Product = product;
                    list.Add(item);
                    orderdetail.ProductID = productId;
                    orderdetail.UserID = session.ID;
                    orderdetail.Name = item.Product.Name;
                    orderdetail.Image = item.Product.Image;
                    orderdetail.Metatitle = item.Product.MetaTitle;
                    orderdetail.Status = true;

                    detaildao.Insert(orderdetail);
                    
                }
                
                
            }
            /*else
            {
                var item = new SaveItem();
                item.Product = product;
                item.Quantity = quantity;
                list = new List<SaveItem>();
                
                list.Add(item);
                Session[SaveSession] = list;
            }*/
            

            return RedirectToAction("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = new SaveDetailDao().Delete(id);
            if (result)
            {
                return RedirectToAction("Index", "Save");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật Sản Phẩm Không thành công");
            }
            return View("Index");

        }

        /*        public JsonResult Delete(long id)
                {
                    var sessionSave = (List<SaveItem>)Session[SaveSession];
                    sessionSave.RemoveAll(x => x.Product.ID == id);
                    Session[SaveSession] = sessionSave;
                    return Json(new
                    {
                        status = true
                    });
                }*/
    }
}