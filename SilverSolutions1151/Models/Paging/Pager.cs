﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Web.Mvc.Ajax;

namespace SilverSolutions1151.Models.Paging
{
    public class Pager
    {
        private ViewContext viewContext;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;
        private readonly AjaxOptions ajaxOptions;

        public Pager(ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary, AjaxOptions ajaxOptions)
        {
            this.viewContext = viewContext;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;
            this.ajaxOptions = ajaxOptions;
        }

        public HtmlString RenderHtml()
        {
            var pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            const int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            sb.Append("<div class=\"pagination\">");
            sb.Append("<ul>");
            // Previous

            if (currentPage > 1)
            {
                sb.Append("<li class=prev>");
                sb.Append("<a href=#>");
                sb.Append(GeneratePageLink("&larr; Previous", currentPage - 1));
                sb.Append("</a>");
                sb.Append("</li>");
            }
            else
            {
                sb.Append("<li class=\"prev disabled\">");
                sb.Append("<a href=#>");
                sb.Append("<span class=\"disabled\">&larr; Previous</span>");
                sb.Append("</a>");
                sb.Append("</li>");
            }


            var start = 1;
            var end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                var below = (currentPage - middle);
                var above = (currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append("<li>");
                sb.Append(GeneratePageLink("1", 1));
                sb.Append("</li>");

                sb.Append("<li>");
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("</li>");

                sb.Append("<li>");
                sb.Append("...");
                sb.Append("</li>");
            }

            for (var i = start; i <= end; i++)
            {

                if (i == currentPage || (currentPage <= 0 && i == 0))
                {
                    sb.Append("<li class=active>");
                    sb.AppendFormat("<a href=#><span class=\"current\">{0}</span></a>", i);
                    sb.Append("</li>");
                }
                else
                {
                    sb.Append("<li>");
                    sb.Append(GeneratePageLink(i.ToString(), i));
                    sb.Append("</li>");
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("<li>");
                sb.Append("...");
                sb.Append("</li>");

                sb.Append("<li>");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1));
                sb.Append("</li>");

                sb.Append("<li>");
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
                sb.Append("</li>");
            }

            // Next
            if (currentPage < pageCount)
            {
                sb.Append("<li class=prev>");
                sb.Append("<a href=#>");
                sb.Append(GeneratePageLink("Next &rarr;", (currentPage + 1)));
                sb.Append("</a>");
                sb.Append("</li>");
            }
            else
            {
                sb.Append("<li class=\"prev disabled\">");
                sb.Append("<a href=#>");
                sb.Append("<span class=\"disabled\">Next &rarr;</span>");
                sb.Append("</a>");
                sb.Append("</li>");
            }

            sb.Append("</ul>");
            sb.Append("</div>");
            return new HtmlString(sb.ToString());
        }

        private string GeneratePageLink(string linkText, int pageNumber)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(linkWithoutPageValuesDictionary) { { "page", pageNumber } };

            // To be sure we get the right route, ensure the controller and action are specified.
            //ar routeDataValues = viewContext.RequestContext.RouteData.Values;
            //if (!pageLinkValueDictionary.ContainsKey("controller") && routeDataValues.ContainsKey("controller"))
            //{
            //    pageLinkValueDictionary.Add("controller", routeDataValues["controller"]);
            //}
            //if (!pageLinkValueDictionary.ContainsKey("action") && routeDataValues.ContainsKey("action"))
            //{
            //    pageLinkValueDictionary.Add("action", routeDataValues["action"]);
            //}

            // 'Render' virtual path.
            //var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(viewContext.RequestContext, pageLinkValueDictionary);

            //if (virtualPathForArea == null)
            //    return null;

            //var stringBuilder = new StringBuilder("<a");

            //if (ajaxOptions != null)
            //    foreach (var ajaxOption in ajaxOptions.ToUnobtrusiveHtmlAttributes())
            //        stringBuilder.AppendFormat(" {0}=\"{1}\"", ajaxOption.Key, ajaxOption.Value);

            //stringBuilder.AppendFormat(" href=\"{0}\">{1}</a>", virtualPathForArea.VirtualPath, linkText);

            //return stringBuilder.ToString();
            return null;
        }
    }
}

