using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class RoleDao
    {
        OnlineShopDbContext db = null;
        public RoleDao()
        {
            db = new OnlineShopDbContext();
        }

        public string Insert(Role entity)
        {
            db.Roles.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Role entity)
        {
            try
            {
                var role = db.Roles.Find(entity.ID);
                role.ID = entity.ID;
                role.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Role ViewDetail(string id)
        {
            return db.Roles.Find(id);
        }
        public IEnumerable<Role> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Role> model = db.Roles;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.ID.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public bool Delete(string id)
        {
            try
            {
                var role = db.Roles.Find(id);
                db.Roles.Remove(role);
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
