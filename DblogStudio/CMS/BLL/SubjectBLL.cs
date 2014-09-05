using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class SubjectBLL
    {
        public static List<Subject> GetList()
        {
            using (var db = new DblogStudioDBContext())
            {
                var list = from e in db.Subject
                           orderby e.Group descending
                           select e;
                return list.ToList();
            }
        }
    }
}