using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;
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
            //            var table = new TagBuilder("table");
            //            table.AddCssClass("table table-striped");
            //            table.InnerHtml = @"
            //                <thead>
            //                    <tr>
            //                        <th>#</th>
            //                        <th>Role</th>
            //                        <th>Thao tác</th>        
            //                    </tr>
            //                </thead>
            //                <tbody>
            //            ";
            //            int i = 1;
            //            foreach (var item in RoleBLL.GetAllRoles())
            //            {
            //                table.InnerHtml += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", i++,item.RoleName,"Xóa");
            //            }
            //            table.InnerHtml += @"</tbody>";
            return Json(RoleBLL.GetAllRoles());
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
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
        #endregion


    }
}
