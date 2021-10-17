using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class AboutDao
    {
        OnlineShopDbContext db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<About> ListAll()
        {
            return db.Abouts.Where(x => x.Status == true).ToList();
        }

        public About GetByID(long id)
        {
            return db.Abouts.Find(id);
        }

        public bool Update(About entity)
        {
            try
            {
                var about = db.Abouts.Find(entity.ID);
                about.Name = entity.Name;
                about.MetaTitle = entity.MetaTitle;
                about.Decscription = entity.Decscription;
                about.Image = entity.Image;
                about.Detail = entity.Detail;
                about.ModifiedBy = entity.ModifiedBy;
                about.ModifiedDate = entity.ModifiedDate;
                about.MetaDescriptions = entity.MetaDescriptions;
                about.MetaKeywords = entity.MetaKeywords;
                about.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public About ViewDetail(long id)
        {
            return db.Abouts.Find(id);
        }
        public IEnumerable<About> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }
        public long Insert(About entity)
        {
            db.Abouts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var menu = db.Abouts.Find(id);
                db.Abouts.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
