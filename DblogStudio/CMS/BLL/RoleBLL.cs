using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class RoleBLL
    {
        public static Role GetByID(int id)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Roles
                                where e.RoleId == id
                                select e).SingleOrDefault();
                    return item;
                }
                catch
                {
                    throw new Exception("Lỗi kiểm tra role");
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
        public static int Create(string roleName)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    if (string.IsNullOrEmpty(roleName))
                        return -2;
                    if (RoleBLL.RoleExists(roleName))
                        return -1;
                    var item = db.Roles.Add(new Role { RoleName = roleName });
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
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
        public static int Update(Role role)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    if (string.IsNullOrEmpty(role.RoleName))
                        return -2;
                    var testItem = (from e in db.Roles
                                    where e.RoleName == role.RoleName && e.RoleId != role.RoleId
                                    select e).SingleOrDefault();
                    if (testItem == null)
                    {
                        var item = (from e in db.Roles
                                    where e.RoleId == role.RoleId
                                    select e).SingleOrDefault();
                        if (item == null)
                            return 0;
                        else
                        {
                            item.RoleName = role.RoleName;
                            db.SaveChanges();
                            return 1;
                        }
                    }
                    else
                        return -1;
                }
                catch
                {
                    throw new Exception("Lỗi update role");
                }

            }
        }

        public static bool Delete(int roleId)
        {
            using (var db = new DblogStudioDBContext())
            {
                try
                {
                    var item = (from e in db.Roles
                                where e.RoleId == roleId
                                select e).SingleOrDefault();
                    if (item == null || item.UserProfiles.Count > 0)
                        return false;
                    else
                    {
                        try
                        {
                            db.Roles.Remove(item);
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
                catch (Exception ex)
                {
                    throw;
                    //throw new Exception("Lỗi lấy danh sách Role");
                }

            }

        }
    }
}