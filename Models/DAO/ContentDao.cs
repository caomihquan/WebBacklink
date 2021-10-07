using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                content.MetaTitle = entity.MetaTitle;
                content.Decscription = entity.Decscription;
                content.Image = entity.Image;
                content.CategoryID = entity.CategoryID;
                content.Detail = entity.Detail;
                content.ModifiedBy = entity.ModifiedBy;
                content.ModifiedDate = DateTime.Now;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.MetaKeywords = entity.MetaKeywords;
                content.TopHot = entity.TopHot;
                content.ViewCount = entity.ViewCount;
                content.Tags = entity.Tags;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
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
