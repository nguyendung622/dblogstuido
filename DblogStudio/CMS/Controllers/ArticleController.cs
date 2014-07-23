using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public string Index()
        {
            return "Hello";
        }
        public string ListArticle(int idCatalogy)
        {
            return idCatalogy.ToString();
        }

    }
}
