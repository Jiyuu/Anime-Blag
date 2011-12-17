using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jiyuu.Aggregation.Common.Data;
using System.Data;
using Argotic.Web;
using Argotic.Syndication;
using Argotic.Extensions.Core;
using System.Timers;
using Jiyuu.Aggregation.Common.SharedData;

namespace Jiyuu.Aggregation
{
    public class AggregationManager
    {
        static AggregationManager()
        {
            updateBlogs();

            Timer t1 = new Timer();
            t1.Elapsed += new ElapsedEventHandler(t1_Elapsed);
            t1.AutoReset = true;
            t1.Interval = 1000 * 60 * 15;
            t1.Start();
        }

        static void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            updateBlogs();
        }

        public static void updateBlogs()
        {
            List<Blog> blogs = DataFactory.GetBlogs(DataStorage.GetAllBlogs());
            foreach (Blog blog in blogs)
            {
                try
                {
                    if (blog.IsActive)
                        switch (blog.FeedType)
                        {
                            case Jiyuu.Aggregation.Common.FeedTypeEnum.ATOM:
                                updateBlogATOMPosts(blog);
                                break;
                            case Jiyuu.Aggregation.Common.FeedTypeEnum.RSS2:
                            default:
                                updateBlogRSS2Posts(blog);
                                break;
                        }
                }
                catch { }

            }
        }

        public static void updateBlogRSS2Posts(Blog blog)
        {
            //ISyndicationResource, IExtensibleSyndicationObject
            //Argotic.Extensions.IExtensibleSyndicationObject a;
            Argotic.Syndication.RssFeed feed = RssFeed.Create(new Uri(blog.FeedURL));
            try
            {
                DataStorage.LogFeedRequest(blog.BlogID, feed.CreateNavigator().OuterXml);
            }
            catch
            { }

            Jiyuu.Aggregation.Common.Data.Lite.BlogPost bp = new Jiyuu.Aggregation.Common.Data.Lite.BlogPost();
            foreach (RssItem item in feed.Channel.Items)
            {
                bp.Summary = getExcerpt(System.Web.HttpUtility.HtmlDecode(item.Description));
                bp.PublicationTS = item.PublicationDate;
                bp.Link = item.Link.ToString();
                bp.Title = item.Title;
                bp.Guid = item.Guid.Value;

                Argotic.Extensions.ISyndicationExtension ise = item.FindExtension(p => p.XmlPrefix == "content");
                if (ise is SiteSummaryContentSyndicationExtension)
                    bp.Content = System.Web.HttpUtility.HtmlDecode(((SiteSummaryContentSyndicationExtension)ise).Context.Encoded);
                else
                    bp.Content = bp.Summary;

                if (item.Author != string.Empty)
                    bp.PostAuthor = item.Author;
                else
                {
                    ise = item.FindExtension(p => p.XmlPrefix == "dc");
                    if (ise is DublinCoreElementSetSyndicationExtension)
                        bp.PostAuthor = ((DublinCoreElementSetSyndicationExtension)ise).Context.Creator;
                }

                bp.Categories = new CategoriesCollection();
                foreach (RssCategory category in item.Categories)
                {
                    bp.Categories.AddCategory(category.Value);
                }
                try
                {
                    DataStorage.SaveBlogPost(bp, blog);
                }
                catch { }
                //bp.PostAuthor
                //((Argotic.Extensions.Core.SiteSummaryContentSyndicationExtension)(item.Summary.ToArray()[1])).Context.Encoded;
            }
        }

        public static void updateBlogATOMPosts(Blog blog)
        {
            Argotic.Syndication.AtomFeed feed = AtomFeed.Create(new Uri(blog.FeedURL));
            try
            {
                DataStorage.LogFeedRequest(blog.BlogID, feed.CreateNavigator().OuterXml);
            }
            catch
            { }

            Jiyuu.Aggregation.Common.Data.Lite.BlogPost bp = new Jiyuu.Aggregation.Common.Data.Lite.BlogPost();
            foreach (AtomEntry item in feed.Entries)
            {
                try
                {
                    if (item.Content != null)
                        bp.Content = System.Web.HttpUtility.HtmlDecode(item.Content.Content);
                    else
                        bp.Content = System.Web.HttpUtility.HtmlDecode(item.Summary.Content);

                    if (item.Summary != null)
                        bp.Summary = getExcerpt(System.Web.HttpUtility.HtmlDecode(item.Summary.Content));
                    else
                        bp.Summary = getExcerpt(bp.Content);
                    bp.PublicationTS = item.PublishedOn;
                    AtomLink link = item.Links.SingleOrDefault(l => l.ContentType == "alternate");
                    if (link != null)
                        bp.Link = link.Uri.ToString();
                    else
                        bp.Link = item.Links[item.Links.Count - 1].Uri.ToString();

                    bp.Title = item.Title.Content;
                    bp.Guid = item.Id.Uri.ToString();


                    if (item.Authors.Count > 0 && item.Authors[0].Name != string.Empty)
                        bp.PostAuthor = item.Authors[0].Name;
                    else
                        if (feed.Authors.Count > 0 && feed.Authors[0].Name != string.Empty)
                            bp.PostAuthor = feed.Authors[0].Name;
                        else
                            bp.PostAuthor = "Anon";

                    bp.Categories = new Jiyuu.Aggregation.Common.SharedData.CategoriesCollection();

                    foreach (AtomCategory cat in item.Categories)
                    {
                        bp.Categories.AddCategory(cat.Term);
                    }

                    try
                    {
                        DataStorage.SaveBlogPost(bp, blog);
                    }
                    catch { }
                }
                catch
                {
                    throw;
                }
                //bp.PostAuthor
                //((Argotic.Extensions.Core.SiteSummaryContentSyndicationExtension)(item.Summary.ToArray()[1])).Context.Encoded;
            }
        }

        public static List<BlogPost> GetPosts(long? blogID, string category)
        {
            DataSet ds = DataStorage.GetPosts(blogID, category);
            List<BlogPost> posts = DataFactory.GetPosts(ds);
            return posts;
        }

        public static List<Blog> GetBlogs()
        {
            DataSet ds = DataStorage.GetBlogs();

            return DataFactory.GetBlogs(ds);
        }


        public static string RemoveContinuationMark(string text, long blogID)
        {
            text = text.TrimEnd();
            int index = 0;
            string[] cMarks = DataStorage.GetCMark(blogID);
            foreach (string cMark in cMarks)
            {
                if ((index = text.LastIndexOf(cMark)) == ((text.Length) - cMark.Length))
                    return text.Remove(text.Length - cMark.Length);
            }
            return text;

        }

        private static string getExcerpt(string text)
        {
            text = Jiyuu.Aggregation.Common.Utils.HtmlRemoval.StripTagsCharArray_WS(text);
            //removes the symbol used the mark(such as "[...]") that theres more text in some blogs

            string[] textArr = text.Split(' ');
            if (textArr.Length <= 56)
                return text;
            else
            {
                return String.Join(" ", textArr, 0, 55);// +" [...]";
            }
        }
    }
}
