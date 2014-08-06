using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class ArticleBLL
    {
        public static Article GetById(int id)
        {
            using (var db = new DblogStudioDBContext())
            {

                var item = (from e in db.Article
                            where e.Id == id
                            select e).SingleOrDefault();
                return item;
            }
        }
        public static List<Article> GetList(int idSubject)
        {
            using (var db = new DblogStudioDBContext())
            {
                if (idSubject == -1)
                {
                    return db.Article.Select(e => e).ToList();
                }
                else
                {
                    var list = from e in db.Article
                               where e.SubjectId == idSubject
                               select e;
                    return list.ToList();
                }
            }
        }
        public static int Create(Article art)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    db.Article.Add(art);
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}