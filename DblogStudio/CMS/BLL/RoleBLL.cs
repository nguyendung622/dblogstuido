using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class RoleBLL
    {
        public static Role Create(string roleName)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = db.Roles.Add(new Role { RoleName = roleName });
                    db.SaveChanges();
                    return item;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static bool RoleExists(string roleName)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Roles
                                where e.RoleName == roleName
                                select e).SingleOrDefault();
                    return (item == null) ? false : true;
                }
                catch
                {
                    throw new Exception("Lỗi kiểm tra role");
                }

            }

        }
        public static List<Role> GetAllRoles()
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = from e in db.Roles
                                select e;
                    return item.ToList();
                }
                catch
                {
                    throw new Exception("Lỗi lấy danh sách Role");
                }

            }

        }
    }
}