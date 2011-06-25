using System;
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


    protected int PostPaging { get {
        int paging = 0;
        if (Request.QueryString.AllKeys.Contains("Page"))
        {
            try { paging = int.Parse(Request.QueryString["Page"])-1; }
            catch { }
        }
        return paging;
    
    } }

    protected int PostsCount { get;set;}

    public  int PageCount{get{    
        return (int)Math.Ceiling((double)((double)PostsCount / (double)pageSize));
    }}

    protected void Page_Load(object sender, EventArgs e)
    {
        long? blogID = null;
        if (Request.QueryString.AllKeys.Contains("BlogID"))
        {
            try { blogID = long.Parse(Request.QueryString["BlogID"]); }
            catch { }
        }

        string category = null;
        if (Request.QueryString.AllKeys.Contains("tag"))
        {
            try { category = Request.QueryString["tag"]; }
            catch { }
        }

        List<BlogPost> posts = AggregationManager.GetPosts(blogID, category);

        PostsRepeater.DataSource = posts.Skip(PostPaging * pageSize).Take(pageSize);
        PostsRepeater.DataBind();

        BlogsRepeater.DataSource = AggregationManager.GetBlogs();
        BlogsRepeater.DataBind();

        PostsCount = posts.Count;

        FooterPager.SetPaging(PageCount, PostPaging);
        TopPager.SetPaging(PageCount, PostPaging);
    }
    

    static string categoryFormat = "<a href=\"?tag={0}\">{0}</a>";
    protected string getCategories()
    {
        BlogPost post= (BlogPost)GetDataItem();

        return string.Join(" ,", post.Categories.CategoriesList.Select(s=>string.Format(categoryFormat,s)).ToArray());
    }
}
