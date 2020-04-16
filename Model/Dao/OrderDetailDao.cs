using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        public MyShopDbContext db = null;

        public OrderDetailDao()
        {
            db = new MyShopDbContext();
        }
        public bool Insert(OrderDetail item)
        {
            try
            {
                db.OrderDetails.Add(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
