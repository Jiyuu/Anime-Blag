﻿using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argotic.Web;
using Argotic.Syndication;
using Jiyuu.Aggregation;
using Jiyuu.Aggregation.Common.Data;
using System.Collections.Generic;
public partial class _Default : System.Web.UI.Page
{
    static int pageSize = 15;

    public string PageTitle { get { return ((DefaultMaster)this.Master).PageTitle; } set { ((DefaultMaster)this.Master).PageTitle = value; } }
    public void setTitle(string title)
    {
        PageTitle = "אנימה בלאג";
        if (title.Trim() != string.Empty)
        {
            PageTitle += " - ";
            PageTitle += title;
        }
    }
    protected int PostPaging
    {
        get
        {
            int paging = 0;
            if (Request.QueryString.AllKeys.Contains("Page"))
            {
                try { paging = int.Parse(Request.QueryString["Page"]) - 1; }
                catch { }
            }
            return paging;

        }
    }

    protected int PostsCount { get; set; }

    public int PageCount
    {
        get
        {
            return (int)Math.Ceiling((double)((double)PostsCount / (double)pageSize));
        }
    }


    static string blogLinkFormat = "<a href=\"?BlogID={0}\">{1}</a>";
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Blog> blogs = AggregationManager.GetBlogs();


        string pageTitle = "<h3 class=\"PostsTitle\">פוסטים";
        if (Request.QueryString.AllKeys.Contains("Feed"))
            Server.Transfer("Feed.aspx");

        long? blogID = null;
        if (Request.QueryString.AllKeys.Contains("BlogID"))
        {
            try { blogID = long.Parse(Request.QueryString["BlogID"]); }
            catch { }
        }
        if (blogID != null)
        {
            try
            {
                Blog blog= blogs.SingleOrDefault(b => b.BlogID == blogID.Value);
                pageTitle += " של: " + string.Format(blogLinkFormat,blog.BlogID,blog.BlogName);
            }
            catch { }
        }
        string category = null;
        if (Request.QueryString.AllKeys.Contains("tag"))
        {
            try { category = Request.QueryString["tag"]; }
            catch { }
        }
        if (category != null)
            pageTitle += " שתוייגו כ: " + string.Format(categoryLinkFormat,category);


        pageTitle += "</h3>";
        if (blogID != null || category != null)
            PostsTitle.Text = pageTitle;
        List<BlogPost> posts = AggregationManager.GetPosts(blogID, category);

        PostsRepeater.DataSource = posts.Skip(PostPaging * pageSize).Take(pageSize);
        PostsRepeater.DataBind();

        BlogsRepeater.DataSource = blogs;
        BlogsRepeater.DataBind();

        PostsCount = posts.Count;

        FooterPager.SetPaging(PageCount, PostPaging);
        TopPager.SetPaging(PageCount, PostPaging);

        try
        {
            if (blogID != null)
                setTitle(blogs.Single(b => b.BlogID == blogID.Value).BlogName);
            else if (category != null)
                setTitle(category);
            else setTitle(string.Empty);
            
        }
        catch { }
    }


    static string categoryLinkFormat = "<a href=\"?tag={0}\">{0}</a>";
    protected string getCategories()
    {
        BlogPost post = (BlogPost)GetDataItem();

        return string.Join(" ,", post.Categories.CategoriesList.Select(s => string.Format(categoryLinkFormat, s)).ToArray());
    }

    protected string getSummary()
    {
        BlogPost post = (BlogPost)GetDataItem();

        return AggregationManager.RemoveContinuationMark(post.Summary, post.Blog.BlogID);
    }
    
}
