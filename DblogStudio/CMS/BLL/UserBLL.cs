using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class UserBLL
    {
        // Count how many posts the blog has  
        //var blog = context.Blogs.Find(1); 
        //var postCount = context.Entry(blog)
        //                      .Collection(b => b.Posts)
        //                      .Query()
        //                      .Count(); 
        public static List<UserProfile> GetAllUser(int roleId = -1)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    if (roleId == -1)
                    {
                        var item = from e in db.UserProfiles.Include("Roles")
                                   select e;
                        return item.ToList();
                    }
                    else
                    {
                        var item = from e in db.UserProfiles.Include("Roles")
                                   where e.Roles.Select(t => t.RoleId).Contains(roleId)
                                   select e;
                        return item.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static bool Delete(int userId)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.UserProfiles
                                where e.UserId == userId
                                select e).SingleOrDefault();
                    db.UserProfiles.Remove(item);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}