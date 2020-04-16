using Model.EF;
using Model.ViewModel;
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

        // Khi ta đặt đây là biến ref thì khi 1 biến đc truyền vào phương thức bày sẽ nhận giá trị sảy ra trong phương thức
        public List<ProductViewModel> GetListProductsByCategoryId(long id,ref int total, int pageIndex = 1, int pageSize = 2)
        {
            total = db.Products.Where(x => x.Status == true && x.CategoryID == id).Count();
            // nối bảng product và category
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == id && a.Status == true
                        select new ProductViewModel()
                        {
                           CateMetaTitle = b.MetaTitle,
                           CateName = b.Name,
                           ID = a.ID,
                           Name = a.Name,
                           MetaTitle = a.MetaTitle,
                           Images = a.Image,
                           Price = a.Price,
                           CreatedDate = a.CreatedDate,
                           Status = a.Status,
                           PromotionPrice = a.PromotionPrice
                        };
            return model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            ;
        }
    }
}
