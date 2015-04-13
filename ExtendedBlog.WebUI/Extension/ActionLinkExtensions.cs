using ExtendedBlog.Model.Data;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ExtendedBlog.WebUI.Extension
{
    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostHeaderLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Header, "FullPost", "Post",
                new
                {
                    postUrl = post.Url
                },
                new
                {
                    title = post.Header
                });
        }

        public static MvcHtmlString PageLinks(this HtmlHelper helper, PageInfo pageInfo,
                                                    Func<int, string> pageUrl)
        {
            if (pageInfo == null)
                return null;

            TagBuilder ulTag = new TagBuilder("ul");

            if (pageInfo.CurrentPageNumber > 1)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(pageInfo.CurrentPageNumber - 1));
                a.InnerHtml = "Предыдущая";
                li.InnerHtml += a.ToString();
                ulTag.InnerHtml += li.ToString();
            }


            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");   //construct <a> tag    
                a.MergeAttribute("href", pageUrl(i));
                a.InnerHtml = i.ToString();
                if (i == pageInfo.CurrentPageNumber)
                    a.AddCssClass("selected");

                li.InnerHtml += a.ToString();
                ulTag.InnerHtml += li.ToString();
            }

            if (pageInfo.CurrentPageNumber < pageInfo.TotalPages)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(pageInfo.CurrentPageNumber + 1));
                a.InnerHtml = "Следующая";
                li.InnerHtml += a.ToString();
                ulTag.InnerHtml += li.ToString();
            }

            return MvcHtmlString.Create(ulTag.ToString());
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "PostsForTag", "Post",
                    new
                    {
                        tagUrl = tag.Url
                    },
                    new
                    {
                        title = tag.Name
                    });
        }
    }
}