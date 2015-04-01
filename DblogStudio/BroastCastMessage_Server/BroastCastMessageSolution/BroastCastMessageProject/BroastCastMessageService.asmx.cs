using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BroastCastMessageProject.DAL;
namespace BroastCastMessageProject
{
    /// <summary>
    /// Summary description for BroastCastMessageService
    /// </summary>
    [WebService(Namespace = "dblogstudio.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BroastCastMessageService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public bool InsertRegistrationID(string registrationID)
        {
            using (var db = new BroastCastMessageDBEntities())
            {
                BCMRegistration reg = new BCMRegistration { DateCreate = DateTime.Now, Enabled = true, RegistrationID = registrationID };
                db.BCMRegistrations.Add(reg);
                db.SaveChanges();
                return true;
            }
        }
        [WebMethod]
        public bool RemoveRegistrationID(string registrationID)
        {
            using (var db = new BroastCastMessageDBEntities())
            {
                var reg = db.BCMRegistrations.Where(e => e.RegistrationID == registrationID).FirstOrDefault();
                if (reg == null)
                    return false;
                reg.Enabled = false;
                db.SaveChanges();
                return true;
            }
        }
        [WebMethod]
        public bool CheckRegistrationID(string registrationID)
        {
            using (var db = new BroastCastMessageDBEntities())
            {
                var reg = db.BCMRegistrations.Where(e => e.RegistrationID == registrationID).FirstOrDefault();
                if (reg == null)
                    return false;
                return true;
            }
        }
        [WebMethod]
        public List<BCMMessage> GetListNews(int page)
        {
            using (var db = new BroastCastMessageDBEntities())
            {
                int totalInPerPage = 10;
                var ls = db.BCMMessages.Where(e => e.Enabled == true).OrderByDescending(e => e.DateCreate).Skip((page - 1) * totalInPerPage).Take(totalInPerPage);
                return ls.ToList();
            }
        }
    }
}
