using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class SaveDetailDao
    {
        OnlineShopDbContext db = null;
        public SaveDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(SaveDetail save)
        {
            try
            {
                db.SaveDetails.Add(save);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<SaveDetail> ListAllPaging(int page, int pageSize)
        {
            return db.SaveDetails.OrderByDescending(x => x.Status==true).ToPagedList(page, pageSize);
        }

        public List<SaveDetail> List(long ID)
        {
            return db.SaveDetails.Where(x => x.UserID == ID).OrderByDescending(x => x.Status == true).ToList();
        }

        public bool Delete(long id)
        {
            try
            {
                var save = db.SaveDetails.Find(id);
                db.SaveDetails.Remove(save);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Check(long id,long user)
        {
            return db.SaveDetails.Count(x => x.ProductID == id && x.UserID == user) > 0;
        }

    }
}
