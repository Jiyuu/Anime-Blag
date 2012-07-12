using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Jiyuu.Aggregation.Common.Data.Lite;
using Jiyuu.Aggregation.Common.Enums;

namespace Jiyuu.Aggregation
{
    static class DataStorage
    {
        //internal static DataSet GetPosts()
        //{
        //    return DAL.gpo();
        //}
        internal static void LogFeedRequest(long blogID, FeedReqTypeEnum reqType, string xmlRssRequest)
        {
            DAL.LogFeedRequest(blogID,reqType ,xmlRssRequest);
        }
        internal static DataSet GetAllBlogs()
        {
            return DAL.GetAllBlogs();
        }

        internal static DataSet GetBlogs()
        {
            return DAL.GetBlogs();
        }
        //internal static DataSet GetAllPosts()
        //{
        //    return DAL.GetAllPosts();
        //}

        internal static DataSet GetPosts(long? blogID,string category)
        {
            return DAL.GetPosts(blogID, category);
        }
        internal static void SaveBlogPost(BlogPost blogPost,Jiyuu.Aggregation.Common.Data.Blog blog)
        {
            DAL.SaveBlogPost(blogPost.PostAuthor, blog.BlogID, blogPost.Title, blogPost.Content, blogPost.Summary, blogPost.PublicationTS, blogPost.Link, blogPost.Guid,blogPost.Categories.ToDelimited());
        }
        internal static void SaveBlogComment(BlogComment blogComment, Jiyuu.Aggregation.Common.Data.Blog blog)
        {
            //DAL.SaveBlogPost(blogPost.PostAuthor, blog.BlogID, blogPost.Title, blogPost.Content, blogPost.Summary, blogPost.PublicationTS, blogPost.Link, blogPost.Guid, blogPost.Categories.ToDelimited());
        }

        static string[] cMarks = new string[] { "[...]", "»","[...]‬" };
        internal static string[] GetCMark(long blogID)
        {
            return (string[])cMarks.Clone();
        }
    }
}
