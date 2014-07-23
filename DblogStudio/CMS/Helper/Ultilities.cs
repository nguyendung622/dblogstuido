using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace CMS.Helper
{
    public static class Ultilities
    {
        /*<li class="active">@Html.ActionLink("Trang chủ", "Index", "Home")</li>
       *  <li class="dropdown">
                             <a href="#" class="dropdown-toggle" data-toggle="dropdown">Chủ đề<span class="caret"></span></a>
                             
                         </li>
       * 
       */
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper,
                                             string text, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentAction,
                              action,
                              StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController,
                              controller,
                              StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            if (routeValues != null)
            {
                li.InnerHtml = (htmlAttributes != null)
                    ? htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            routeValues,
                                            htmlAttributes).ToHtmlString()
                    : htmlHelper.ActionLink(text,
                                            action,
                                            controller,
                                            routeValues).ToHtmlString();
            }
            else
            {
                li.InnerHtml = htmlHelper.ActionLink(text,
                                                     action,
                                                     controller).ToHtmlString();
            }
            return MvcHtmlString.Create(li.ToString());
        }
        //<ul class="dropdown-menu" role="menu">
        //                         <li class="dropdown-header">Tin học đại cương</li>
        //                         <li><a href="#">[TIN1013]Khối tự nhiên <span class="badge">42</span></a></li>
        //                         <li><a href="#">[TIN1023]Khối xã hội</a></li>
        //                         <li class="divider"></li>
        //                         <li class="dropdown-header">Nguyên lý</li>
        //                         <li><a href="#">Kỹ nghệ phần mềm</a></li>
        //                         <li class="divider"></li>
        //                         <li class="dropdown-header">Lập trình nâng cao</li>
        //                         <li><a href="#">Di động</a></li>
        //                         <li><a href="#">Web</a></li>
        //                         <li><a href="#">Desktop</a></li>

        //                     </ul>
        public static MvcHtmlString MenuDropDown(this HtmlHelper htmlHelper)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("dropdown");
            var routeData = htmlHelper.ViewContext.RouteData;
            //var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            if (string.Equals(currentController,
                              "Article",
                              StringComparison.OrdinalIgnoreCase))
                li.AddCssClass("active");
            li.InnerHtml = @"<a href='#' class='dropdown-toggle' data-toggle='dropdown'>Chủ đề<span class='caret'></span></a>";
            var ul = new TagBuilder("ul");
            ul.AddCssClass("dropdown-menu");
            ul.MergeAttribute("role", "menu");
            for (int i = 0; i < 5; i++)
            {
                var a = new TagBuilder("a");
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                var url = urlHelper.Action("ListArticle", "Article", new { idCatalogy = i });
                a.MergeAttribute("href", url);
                a.InnerHtml = "Tin học đại cương<span class='badge'>42</span>";
                ul.InnerHtml += "<li>" + a.ToString() + "</li>";
            }
            li.InnerHtml += ul.ToString();
            return MvcHtmlString.Create(li.ToString());
        }
    }
}