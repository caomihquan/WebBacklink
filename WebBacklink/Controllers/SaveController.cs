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
    
    public class SaveController : Controller
    {
        private const string SaveSession = "SaveSession";
        // GET: Save
        public ActionResult Index()
        {
            var save = Session[SaveSession];

            var list = new List<SaveItem>();

            if (save != null)
            {
                list = (List<SaveItem>)save;

            }
            return View(list);

        }


        public JsonResult DeleteAll()
        {
            Session[SaveSession] = null;
            return Json(new
            {
                status = true
            }) ;
        }

        public JsonResult Delete(long id)
        {
            var sessionSave = (List<SaveItem>)Session[SaveSession];
            sessionSave.RemoveAll(x => x.Product.ID == id);
            Session[SaveSession] = sessionSave;
            return Json(new
            {
                status = true
            });
        }

        
        public ActionResult AddItem(long productId,int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            
            var user = Session[CommonConstants.USER_SESSION];
            var save = Session[SaveSession];
            
            if (save != null)
            {
                var list = (List<SaveItem>)save;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new SaveItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[SaveSession]  = list;            
            }
            else
            {
                var item = new SaveItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<SaveItem>();
                var item2 = new Save();
                list.Add(item);
                Session[SaveSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}