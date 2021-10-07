using Models.EF;
using Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.MetaTitle = entity.MetaTitle;
                product.Price = entity.Price;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<Product> ListPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListAllProduct()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Product> ListFeatureProduct (int top)
        {
            return db.Products.Where(x => x.TopHot != null&&x.TopHot>DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public List<ProductViewModel> ListByCategoryId(long categoryID,ref int totalRecord,int pageIndex=1,int pageSize=1)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == categoryID
                        select new ProductViewModel()
                        {
                            CateMetaTitle = b.MetaTitle,
                            CateName=b.Name,
                            CreatedDate= a.CreatedDate,
                            ID=a.ID,
                            Images=a.Image,
                            Name=a.Name,
                            MetaTitle=a.MetaTitle,
                            Price=a.Price


                        };
            model.OrderByDescending(x=>x.CreatedDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model.ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
