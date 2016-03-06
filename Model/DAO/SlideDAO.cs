using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SlideDAO
    {
        OnlineShopDbContext db = null;
        public SlideDAO()
       {
           db = new OnlineShopDbContext();

       }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(p => p.Status == true).OrderBy(p => p.DisplayOrder).ToList();
        }
    }
}
