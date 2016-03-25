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

            //Insert ContentTag and Tag
            InsertTagContentTag(obj.ID, obj.Tags);

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

        public bool Update(Content obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.Name))
                    obj.MetaTitle = StringHelper.ToUnsignString(obj.Name);

                var objContent = db.Contents.Where(p => p.ID == obj.ID).FirstOrDefault();
                objContent.Name = obj.Name;
                objContent.MetaTitle = obj.MetaTitle;
                objContent.Description = obj.Description;
                objContent.Image = obj.Image;
                objContent.CategoryID = obj.CategoryID;
                objContent.Detail = obj.Detail;
                objContent.Status = obj.Status;
                objContent.TopHot = obj.TopHot;
                objContent.Tags = obj.Tags;
                objContent.Language = obj.Language;
                objContent.ModifiedBy = obj.ModifiedBy;
                objContent.ModifiedDate = obj.ModifiedDate;

                db.SaveChanges();

                //Remove all content_tag by contentID
                if(RemoveAllContentTag(obj.ID))
                {
                    InsertTagContentTag(obj.ID, obj.Tags);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool RemoveAllContentTag(long ContentID)
        {
            try
            {
                var lstContentTag = db.ContentTags.Where(p => p.ContentID == ContentID);
                if(lstContentTag.Any())
                {
                    db.ContentTags.RemoveRange(lstContentTag);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool InsertTagContentTag(long id, string strTag)
        {
            try
            {
                if (!string.IsNullOrEmpty(strTag))
                {
                    if (strTag.Contains(","))
                    {
                        string[] tag = strTag.Split(',');

                        for (int i = 0; i < tag.Count(); i++)
                        {
                            var objTag = new Tag()
                            {
                                ID = StringHelper.ToUnsignString(tag[i]),
                                Name = tag[i]
                            };

                            //Insert tag
                            if (!CheckTag(StringHelper.ToUnsignString(tag[i])))
                            {
                                db.Tags.Add(objTag);
                            }
                            //Insert contenttag
                            var objContenTag = new ContentTag()
                            {
                                ContentID = id,
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
                            ID = StringHelper.ToUnsignString(strTag),
                            Name = strTag
                        };

                        //Insert tag
                        if (!CheckTag(StringHelper.ToUnsignString(strTag)))
                        {
                            db.Tags.Add(objTag);
                        }
                        //Insert contenttag
                        var objContenTag = new ContentTag()
                        {
                            ContentID = id,
                            TagID = StringHelper.ToUnsignString(strTag)
                        };
                        db.ContentTags.Add(objContenTag);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var objContent = db.Contents.Find(id);
                if (objContent != null) db.Contents.Remove(objContent);

                //Remove list ContentTag
                var lstContentTags = db.ContentTags.Where(p => p.ContentID == id);
                if (lstContentTags.Any()) db.ContentTags.RemoveRange(lstContentTags);

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var objContent = db.Contents.Find(id);
            objContent.Status = !objContent.Status;
            db.SaveChanges();

            return objContent.Status;
        }

        public List<Content> ListPaging(ref int total, int page, int pageSize)
        {
            total = db.Contents.Where(p => p.Status == true).Count();
            return db.Contents.Where(p => p.Status == true).OrderByDescending(p => p.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Tag> GetAllTagByContenId(long id)
        {
            try
            {
                var lstContentTag = db.ContentTags.Where(p => p.ContentID == id) ;
                //var lstTag = db.Tags.ToList();
                //var lst = lstContentTag.Join(db.Tags, (ct => ct.TagID), (t => t.ID), ((ct, t) => new {id = t.ID, name = t.Name }))
                //    .AsEnumerable().Select(p=> new Tag{ ID = p.id, Name = p.name}).ToList();
                //var query = (from ct in db.ContentTags
                //            join t in db.Tags on ct.TagID equals t.ID
                //            select new
                //            {
                //                ID = t.ID,
                //                Name = t.Name
                //            }).AsEnumerable().Select(p=> new Tag {
                //                ID = p.ID,
                //                Name = p.Name
                //            }).ToList();

                var query = (from ct in db.ContentTags
                             join t in db.Tags on ct.TagID equals t.ID
                             where ct.ContentID == id
                             select t).ToList();

                return query;
            }
            catch (Exception ex)
            {
                return new List<Tag>();
            }
           
                      
        }

        public List<Content> ListPagingTag(string tagID,ref int total, int page, int pageSize)
        {
            var lstContentId = db.ContentTags.Where(p => p.TagID.Equals(tagID)).Select(x => x.ContentID).Distinct();

            //var lstContent = lstContentId.Join(db.Contents, x => x, y => y.ID, (x, y) => new { y }).AsEnumerable().Select(new Content(y)).ToList();

            var lst = (from id in lstContentId
                       join c in db.Contents on id equals c.ID
                       where c.Status == true
                       select c).ToList();

            total = lst.Count();
            return lst.OrderByDescending(p => p.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public Tag GetTagByTagId(string tagId)
        {
            return db.Tags.Where(p => p.ID == tagId).FirstOrDefault();
        }
    }
    //public class TagNew
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}
}
