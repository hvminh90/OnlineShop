using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        OnlineShopDbContext db = null;
        public MenuDAO()
        {
            db = new OnlineShopDbContext();

        }
            
        public List<Menu> ListByGroupId(int group)
        {

            return db.Menus.Where(p=>p.TypeID == group && p.Status == true).OrderBy(p=>p.DisplayOrder).ToList();
        }

    }
}
