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
            return Json(RoleBLL.GetAllRoles());
        }

        [HttpPost]
        public int CreateRole(string roleName)
        {
            /*
             * return 1 nếu thành công
             * return -1 nếu trùng
             * return 0 nếu thất bại
             */
            if (!string.IsNullOrEmpty(roleName))
            {
                try
                {

                    if (RoleBLL.RoleExists(roleName))
                        return -1;
                    else
                    {

                        return (RoleBLL.Create(roleName) == null) ? 0 : 1;
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public int DeleteRole(int roleId)
        {
            /*
             * return 0 nếu thất bại, 1 nếu thành công
             */
            return RoleBLL.Delete(roleId) ? 1 : 0;
        }

        public ActionResult GetUpdateRole(int roleId)
        {
            //return PartialView("UpdateRole", RoleBLL.GetByID(roleId));
            return Json(RoleBLL.GetByID(roleId));
        }
        public int PostUpdateRole(Role role)
        {
            if (RoleBLL.Update(role) == null)
                return 0;
            else
                return 1;
        }
        #endregion


    }
}
