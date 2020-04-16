using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedBackDao
    {
        MyShopDbContext db = null;

        public FeedBackDao()
        {
            db = new MyShopDbContext();
        }

        public bool Insert(FeedBack fb)
        {
            try
            {
                db.FeedBacks.Add(fb);
                db.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }

        }
    }
}
