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
            var lsRole = RoleBLL.GetAllRoles();
            return View(lsRole);
        }

        public ActionResult GetListUser(int roleId = -1)
        {
            var list = UserBLL.GetAllUser(roleId);

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

        #region

        public ActionResult SubjectManage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListSubject()
        {
            var list = SubjectBLL.GetAllSubjects();
            return Json(list);
        }

        //[HttpGet]
        public ActionResult CreateOrUpdateSubject(int idSubject = -1)
        {
            if (idSubject != -1)
            {
                var subject = SubjectBLL.GetByID(idSubject);
                return PartialView("CreateOrUpdateSubject", subject);
            }
            return PartialView("CreateOrUpdateSubject", new Subject { Id = -1, Group = "Đại cương" });
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
        public int CreateSubject(string name, string group)
        {
            return SubjectBLL.Create(name, group);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>
        /// 0 nếu thất bại
        /// 1 nếu thành công
        /// </returns>
        public int DeleteSubject(int subjectId)
        {
            return SubjectBLL.Delete(subjectId) ? 1 : 0;
        }

        public ActionResult GetUpdateSubject(int subjectId)
        {
            return Json(SubjectBLL.GetByID(subjectId));
        }

        public int PostUpdateSubject(int id, string name, string group)
        {
            Subject subject = new Subject { Id = id, Name = name, Group = group };
            return SubjectBLL.Update(subject);
        }

        #endregion
    }
}
