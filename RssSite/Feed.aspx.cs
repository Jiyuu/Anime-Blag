using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argotic.Syndication;
using System.IO;
using Jiyuu.Aggregation;
using Jiyuu.Aggregation.Common.Data;
using System.Xml.Linq;
using System.Xml;

public partial class Feed : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        RssFeed feed = new RssFeed();

        feed.Channel.Link = new Uri("http://blag.Jiyuu.org/");
        feed.Channel.Title = "אנימה בלאג";
        feed.Channel.Description = "סיכום הפוסטים האחרונים שאונדקסו בבלאג";

        List<BlogPost> posts = AggregationManager.GetPosts(null, null).Take(15).ToList();

        RssItem item;
        foreach (BlogPost post in posts)
        {
            item = new RssItem();
            item.Title = post.Title;
            item.Link = new Uri(post.Link);
            item.Description = post.Summary;
            
            Argotic.Extensions.Core.SiteSummaryContentSyndicationExtension contentExt = new Argotic.Extensions.Core.SiteSummaryContentSyndicationExtension();
            contentExt.Context.Encoded = "";// HttpUtility.HtmlDecode(string.Format(@"<dir=""rtl"" style=""text-align: right;"" trbidi=""on"">{0}</div>", post.Summary));
            //Argotic.Extensions.Core.SiteSummaryContentItem content = new Argotic.Extensions.Core.SiteSummaryContentItem();
            //content.Content = new XCData(string.Format(@"<dir=""rtl"" style=""text-align: right;"" trbidi=""on"">{0}</div>", post.Summary)).ToString();
            //contentExt.Context.Items.Add(content);
            item.AddExtension(contentExt);
            item.PublicationDate = post.PublicationTS;
            feed.Channel.AddItem(item);
            
        }

        //using (FileStream stream = new FileStream("SimpleRssFeed.xml", FileMode.Create, FileAccess.Write))
        //{
        Response.ContentType = "application/rss+xml";
        Response.Write(feed.CreateNavigator().OuterXml);

        //}

    }
    public static string XmlEscape(string unescaped) {   

        
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument(); var node = doc.CreateCDataSection("root"); node.InnerText = unescaped; return node.InnerXml; }
}