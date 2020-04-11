using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SlideDao
    {
        MyShopDbContext db = null;
        public SlideDao()
        {
            db = new MyShopDbContext();
        }

        public List<Slide> GetSlide()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
