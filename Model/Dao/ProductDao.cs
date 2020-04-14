using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        MyShopDbContext db = null;
        public ProductDao()
        {
            db = new MyShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.PromotionPrice > 0 && x.PromotionPrice != null).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public Product GetProductById(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> ListRelateProduct(long productId, int top)
        {
            var pd = db.Products.Find(productId);
            var list = db.Products.Where(x => x.ID != pd.ID && x.CategoryID == pd.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
            return list;
        }
    }
}
