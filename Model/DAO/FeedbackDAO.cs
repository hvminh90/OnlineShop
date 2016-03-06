using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class FeedbackDAO
    {
        OnlineShopDbContext db = null;
        public FeedbackDAO()
        {
            db = new OnlineShopDbContext();

        }

        public long InsertFeedBack(FeedBack obj)
        {
            db.FeedBacks.Add(obj);
            db.SaveChanges();
            return obj.ID;
        }
    }
}
