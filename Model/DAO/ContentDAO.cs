using Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext db = null;
        public ContentDAO()
        {
            db = new OnlineShopDbContext();

        }

        public long InsertContent(Content obj)
        {
            if (!string.IsNullOrEmpty(obj.Name))
                obj.MetaTitle = StringHelper.ToUnsignString(obj.Name);

            db.Contents.Add(obj);
            db.SaveChanges();

            if (!string.IsNullOrEmpty(obj.Tags))
            {
                if (obj.Tags.Contains(","))
                {
                    string[] tag = obj.Tags.Split(',');

                    for (int i = 0; i < tag.Count(); i++)
                    {
                        var objTag = new Tag()
                        {
                            ID = StringHelper.ToUnsignString(tag[i]),
                            Name = tag[i]
                        };

                        //Insert tag
                        if(!CheckTag(StringHelper.ToUnsignString(tag[i])))
                        {
                            db.Tags.Add(objTag);
                        }
                        //Insert contenttag
                        var objContenTag = new ContentTag()
                        {
                            ContentID = obj.ID,
                            TagID = StringHelper.ToUnsignString(tag[i])
                        };
                        db.ContentTags.Add(objContenTag);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var objTag = new Tag()
                    {
                        ID = StringHelper.ToUnsignString(obj.Tags),
                        Name = obj.Tags
                    };

                    //Insert tag
                    if (!CheckTag(StringHelper.ToUnsignString(obj.Tags)))
                    {
                        db.Tags.Add(objTag);
                    }
                    //Insert contenttag
                    var objContenTag = new ContentTag()
                    {
                        ContentID = obj.ID,
                        TagID = StringHelper.ToUnsignString(obj.Tags)
                    };
                    db.ContentTags.Add(objContenTag);
                    db.SaveChanges();
                }
            }


            return obj.ID;
        }

        public bool CheckTag(string tagId)
        {
            return db.Tags.Where(p => p.ID == tagId).Count() > 0;
        }


        public IEnumerable<Content> ListAllPaging(int page, int pageSize, string searchString)
        {
            IEnumerable<Content> contents = db.Contents.ToList();
            if (!string.IsNullOrEmpty(searchString))
                contents = contents.Where(x => x.Name.Contains(searchString) || x.Detail.Contains(searchString));

            contents = contents.OrderByDescending(p => p.CreatedDate);
            return contents.ToPagedList(page, pageSize);
        }

        public Content GetContenById(long id)
        {
            var obj = db.Contents.Find(id);
            if (obj != null) return obj;
            else return new Content();
        }

    }
}
