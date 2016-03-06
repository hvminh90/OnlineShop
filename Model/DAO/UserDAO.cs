using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAO
{
   public class UserDAO
    {
       OnlineShopDbContext db = null;
       public UserDAO()
       {
           db = new OnlineShopDbContext();

       }

       public long Insert(User entity)
       {
           db.Users.Add(entity);
           db.SaveChanges();
           return entity.ID;
       }
       public User GetByUserName(string userName)
       {
           return db.Users.SingleOrDefault(p => p.UserName == userName);
       }
       public int Login(string userName, string passWord)
       {
           var result = db.Users.SingleOrDefault(x => x.UserName == userName);
           if (result == null)
               return 0;
           else
           {
               if (result.Status == false)
                   return -1;
               else
               {
                   if (result.Password == passWord)
                       return 1;
                   else return -2;
               }
           }
       }

       public IEnumerable<User> ListAllPaging(int page, int pageSize, string searchString)
       {
          IEnumerable<User> users = db.Users.ToList();
           if (!string.IsNullOrEmpty(searchString))
               users = users.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));

           users = users.OrderByDescending(p => p.CreatedDate);
           return users.ToPagedList(page,pageSize);
       }

       public User GetByID(int id)
       {
           return db.Users.FirstOrDefault(p => p.ID == id);

       }
       public bool Update(User entity)
       {
           try
           {
               var user = db.Users.FirstOrDefault(p => p.ID == entity.ID);
               user.Name = entity.Name;
               user.Address = entity.Address;
               user.Phone = entity.Phone;
               user.Email = entity.Email;
               user.ModifiedDate = DateTime.Now;
               user.ModifiedBy = entity.ModifiedBy;

               db.SaveChanges();
               return true;
           }
           catch (Exception ex)
           {

               return false;
           }
       }

       public bool Delete(int id)
       {
           var user=  db.Users.FirstOrDefault(p => p.ID == id);
           db.Users.Remove(user);
           db.SaveChanges();
           return true;
       }

       public bool ChangeStatus(int id)
       {
           var user = db.Users.SingleOrDefault(x => x.ID == id);
           user.Status = !user.Status;
           db.SaveChanges();

           return user.Status;
       }

       public bool CheckEmail(string email)
       {
           var user = db.Users.Where(p => p.Email.Equals(email)).FirstOrDefault();
           if (user != null) return true;

           return false;
       }
       public bool CheckUsername(string Username)
       {
           var user = db.Users.Where(p => p.UserName.Equals(Username)).FirstOrDefault();
           if (user != null) return true;

           return false;
       }

       public long InsertFacebook(User obj)
       {
           var user = db.Users.Where(p => p.UserName == obj.UserName).FirstOrDefault();
           if(user ==  null)
           {
               db.Users.Add(obj);
               db.SaveChanges();
               return obj.ID;
           }
           else
            return user.ID;
       }
    }
}
