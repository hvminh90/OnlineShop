using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDbContext db = null;
        public OrderDetailDAO()
       {
           db = new OnlineShopDbContext();

       }

        public bool Insert(OrderDetail orderDetail)
        {
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();
            return true;
        }
    }
}
