using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class SubjectBLL
    {
        public static Subject GetByID(int id)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Subject
                                where e.Id == id
                                select e).SingleOrDefault();
                    return item;
                }
                catch
                {
                    throw new Exception("Lỗi kiểm tra subject");
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>
        /// -2: nếu roleName rỗng
        /// -1: nếu trùng
        /// 0: nếu thất bại
        /// 1: nếu thành công
        /// </returns>
        public static int Create(string subjectName, string group)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    if (string.IsNullOrEmpty(subjectName))
                        return -2;
                    if (SubjectBLL.SubjectExists(subjectName))
                        return -1;
                    var item = db.Subject.Add(new Subject { Name = subjectName, Group = group });
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static bool SubjectExists(string subjectName)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Subject
                                where e.Name == subjectName
                                select e).SingleOrDefault();
                    return (item == null) ? false : true;
                }
                catch
                {
                    throw new Exception("Lỗi kiểm tra subject");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns>
        /// -2: nếu tên rỗng
        /// -1: nếu trùng RoleName
        /// 0: nếu cập nhật thất bại (không tìm thấy role)
        /// 1: thành công
        /// </returns>
        public static int Update(Subject subject)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    if (string.IsNullOrEmpty(subject.Name))
                        return -2;
                    var testItem = (from e in db.Subject
                                    where e.Name == subject.Name && e.Id != subject.Id
                                    select e).SingleOrDefault();
                    if (testItem == null)
                    {
                        var item = (from e in db.Subject
                                    where e.Id == subject.Id
                                    select e).SingleOrDefault();
                        if (item == null)
                            return 0;
                        else
                        {
                            item.Name = subject.Name;
                            item.Group = subject.Group;
                            db.SaveChanges();
                            return 1;
                        }
                    }
                    else
                        return -1;
                }
                catch
                {
                    throw new Exception("Lỗi update subject");
                }

            }
        }

        public static bool Delete(int subjectId)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Subject.Include("Articles")
                                where e.Id == subjectId
                                select e).SingleOrDefault();
                    if (item == null || item.Articles.Count > 0)
                        return false;
                    else
                    {
                        try
                        {
                            db.Subject.Remove(item);
                            db.SaveChanges();
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

        }

        public static List<Subject> GetAllSubjects()
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = from e in db.Subject
                               orderby e.Group descending
                               select e;
                    return item.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                    //throw new Exception("Lỗi lấy danh sách Role");
                }

            }

        }
    }

}