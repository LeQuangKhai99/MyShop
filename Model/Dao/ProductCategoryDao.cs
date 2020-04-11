using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        MyShopDbContext db = null;

        public ProductCategoryDao()
        {
            db = new MyShopDbContext();
        }

        public List<ProductCategory> GetProductCategory()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
