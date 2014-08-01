using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;
using CMS.Models;
namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        #region Role manage

        public ActionResult RoleManage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListRole()
        {
            var list = RoleBLL.GetAllRoles();
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>
        /// -2 nếu tên rỗng
        /// -1 nếu trùng
        /// 0 nếu thất bại
        /// 1 nếu thành công
        /// </returns>
        [HttpPost]
        public int CreateRole(string roleName)
        {
            return RoleBLL.Create(roleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>
        /// 0 nếu thất bại
        /// 1 nếu thành công
        /// </returns>
        public int DeleteRole(int roleId)
        {
            return RoleBLL.Delete(roleId) ? 1 : 0;
        }

        public ActionResult GetUpdateRole(int roleId)
        {
            return Json(RoleBLL.GetByID(roleId));
        }

        public int PostUpdateRole(Role role)
        {
            return RoleBLL.Update(role);
        }
        #endregion

        #region UserManage

        public ActionResult UserManage()
        {
            return View();
        }

        public ActionResult GetListUser()
        {
            var list = UserBLL.GetAllUser();

            return Json(list.Select(e => new
            {
                UserId = e.UserId,
                UserName = e.UserName,
                FullName = e.FullName,
                BirthDay = e.BirthDay,
                Department = e.Department,
                StudentId = e.StudentId,
                Roles = e.Roles.Select(t => t.RoleName)
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// 0: Nếu  thất bại
        /// 1: Nếu thành công
        /// </returns>
        public int DeleteUser(int userId)
        {
            return UserBLL.Delete(userId) ? 1 : 0;
        }

        #endregion
    }
}
