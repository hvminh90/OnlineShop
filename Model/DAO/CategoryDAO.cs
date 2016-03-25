using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.DAO
{
    public class CategoryDAO
    {
         OnlineShopDbContext db = null;
         public CategoryDAO()
       {
           db = new OnlineShopDbContext();

       }

        public List<Category> GetAllCategory()
        {
            
            return db.Categories.Where(x=>x.Status== true).ToList();
        }

        public XElement ConvertCategoryToXML(List<Category> lst)
        {
            XElement xml = new XElement("Root");
            if(lst.Count > 0)
            {
                foreach (var obj in lst)
                {
                    XElement item = new XElement("Categorys",
                                                 new XElement("ID",obj.ID),
                                                 new XElement("Name", obj.Name),
                                                 new XElement("MetaTitle", obj.MetaTitle),
                                                 new XElement("ParentID", obj.ParentID),
                                                 new XElement("DisplayOrder", obj.DisplayOrder),
                                                 new XElement("SeoTitle", obj.SeoTitle),
                                                 new XElement("CreatedDate", obj.CreatedDate),
                                                 new XElement("CreatedBy", obj.CreatedBy),
                                                 new XElement("ModifiedDate", obj.ModifiedDate),
                                                 new XElement("ModifiedBy", obj.ModifiedBy),
                                                 new XElement("MetaKeywords", obj.MetaKeywords),
                                                 new XElement("MetaDescriptions", obj.MetaDescriptions),
                                                 new XElement("Status", obj.Status),
                                                 new XElement("ShowOnHome", obj.ShowOnHome),
                                                 new XElement("Language", obj.Language));
                    xml.Add(item);
                }
            }
            return xml;
        }

        public long InsertListCategory(List<Category>lst)
        {
            XElement xml = ConvertCategoryToXML(lst);
            string strXml = Common.StringHelper.ParseXML(lst);

            var param = new SqlParameter("Category", strXml);
            //var result = db.Database.ExecuteSqlCommand("sp_Insert_Category @Category", param); //sẽ ra số dòng được insert/Update/Delete
            //var result = db.Database.SqlQuery<string>("sp_Insert_Category @Category", param).FirstOrDefault<string>();// trả về 1 thành công. -1 lỗi
            return 1;
        }

        public long Insert(Category model)
        {
            db.Categories.Add(model);
            db.SaveChanges();

            return model.ID;
        }

        public Category GetCategory(long id)
        {
            return db.Categories.Where(p => p.ID == id).FirstOrDefault();
        }

        public bool Delete(long id)
        {
            try
            {
                var obj = db.Categories.Where(p => p.ID == id).FirstOrDefault();
                db.Categories.Remove(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        public bool Edit(Category model)
        {
            try
            {
                var obj = db.Categories.Where(p => p.ID == model.ID).FirstOrDefault();
                obj.Name = model.Name;
                obj.MetaTitle = model.MetaTitle;
                obj.ParentID = model.ParentID;
                obj.ModifiedBy = model.ModifiedBy;
                obj.ModifiedDate = model.ModifiedDate;
                obj.Status = model.Status;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
