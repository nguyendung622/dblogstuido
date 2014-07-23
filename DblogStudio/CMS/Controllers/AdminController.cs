using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        public ActionResult RoleManage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListRole()
        {
            //Roles.GetAllRoles()
            var list = new List<Role>();
            list.Add(new Role { Name = "Admin 1" });
            list.Add(new Role { Name = "Admin 2" });
            list.Add(new Role { Name = "Admin 3" });
            return Json(list);
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public string CreateRole(string roleName)
        {
            return roleName;
        }
        public class Role
        {
            public string Name { get; set; }
        }

    }
}
