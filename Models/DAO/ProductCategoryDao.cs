using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(ProductCategory entity)
        {
            try
            {
                var productcategory = db.ProductCategories.Find(entity.ID);
                productcategory.Name = entity.Name;
                productcategory.MetaTitle = entity.MetaTitle;
                productcategory.SeoTitle = entity.SeoTitle;
                productcategory.ModifiedBy = entity.ModifiedBy;
                productcategory.ModifiedDate = DateTime.Now;
                productcategory.MetaDescriptions = entity.MetaDescriptions;
                productcategory.MetaKeywords = entity.MetaKeywords;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public ProductCategory GetByID(string Name)
        {
            return db.ProductCategories.SingleOrDefault(x => x.Name == Name);
        }

        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }
        public IEnumerable<ProductCategory> ListAllPaging( int page, int pageSize)
        {

            return db.ProductCategories.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var productcategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productcategory);
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
