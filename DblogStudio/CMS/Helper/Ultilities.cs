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

        //public static MvcHtmlString BreadCumb(this HtmlHelper htmlHelper)
        //{
        //    /*<li><a href="#">Trang chủ</a></li>
        //    <li><a href="#">Chủ đề</a></li>
        //    <li class="active">Tin học đại cương</li>
        //     */

        //}
    }
}