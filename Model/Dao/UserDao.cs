using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        MyShopDbContext db = null;
        public UserDao()
        {
            db = new MyShopDbContext();
        }

        // Phân trang
        public IEnumerable<User> ListAllPaging(string search, int page, int pageSize)
        {
            // Lấy dữ liệu trên server vèe
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.UserName.Contains(search) || x.Name.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public int CheckIssetUserName(User user)
        {
            var data = db.Users.SingleOrDefault(x => x.UserName == user.UserName);
            if (data != null)
            {
                return 0;
            }
            return 1;
        }
        public int CheckInsert(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return 1;
            }
            if (string.IsNullOrEmpty(user.Address))
            {
                return 2;
            }
            if (string.IsNullOrEmpty(user.Phone))
            {
                return 3;
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                return 4;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return 5;
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return 6;
            }

            return 0;
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public int CkeckChangeUserName(User user)
        {
            var obj = db.Users.SingleOrDefault(x => x.ID == user.ID && x.UserName == user.UserName);
            if (obj == null)
            {
                return 1;
            }
            return 0;
        }

        public int CheckUpdate(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return 1;
            }
            if (string.IsNullOrEmpty(user.Address))
            {
                return 2;
            }
            if (string.IsNullOrEmpty(user.Phone))
            {
                return 3;
            }

            return 0;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                user.Name = entity.Name;
                user.Status = entity.Status;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = entity.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public User GetUserByUserName(string username)
        {
            // Trả về 1 bản ghi nếu trùng khớp
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }

        public User GetUserById(int id)
        {
            // trả về user có khóa chính = giá trị id
            return db.Users.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}
