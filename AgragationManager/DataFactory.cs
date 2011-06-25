using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Jiyuu.Aggregation.Common.Data;
using Jiyuu.Aggregation.Common.SharedData;

namespace Jiyuu.Aggregation
{
    public static class DataFactory
    {
        internal static List<BlogPost> GetPosts(System.Data.DataSet ds)
        {
            List<BlogPost> tmpList = new List<BlogPost>();

            if (ds.Tables.Count <= 2)
                return tmpList;

            Dictionary<long,Blog> blogsDic = GetBlogs(ds.Tables[1]).ToDictionary(key=>key.BlogID);

            BlogPost tmpPost;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tmpPost = new BlogPost();
                tmpPost.Blog = blogsDic[(long)dr["BlogID"]];
                tmpPost.Content = dr["Body"].ToString();
                tmpPost.Guid = dr["Guid"].ToString();
                tmpPost.Link = dr["Link"].ToString();
                tmpPost.PostAuthor = new BlogAuthor(dr["AuthorName"].ToString());
                tmpPost.PublicationTS = (DateTime)dr["PublicationTS"];
                tmpPost.PostID = (long)dr["PostID"];
                tmpPost.Summary = dr["Summary"].ToString();
                tmpPost.Title = dr["Subject"].ToString();
                tmpPost.Categories= new CategoriesCollection(dr["Categories"].ToString());
                tmpList.Add(tmpPost);
            }

            return tmpList;
        }

        internal static List<Jiyuu.Aggregation.Common.Data.Blog> GetBlogs(System.Data.DataTable datatable)
        {
            List<Jiyuu.Aggregation.Common.Data.Blog> tmpList = new List<Jiyuu.Aggregation.Common.Data.Blog>();
            Blog tmpBlog;
            foreach (DataRow dr in datatable.Rows)
            {
                tmpBlog = new Blog();
                tmpBlog.BlogID = (long)dr["BlogID"];
                tmpBlog.BlogName = dr["BlogName"].ToString();
                tmpBlog.FeedType =(Jiyuu.Aggregation.Common.FeedTypeEnum) dr["FeedType"];
                tmpBlog.FeedURL = dr["FeedURL"].ToString();
                tmpBlog.HomepageURL = dr["HomepageURL"].ToString();
                tmpBlog.LastUpdateTS = (DateTime)dr["LastUpdateTS"];
                tmpBlog.UpdateInterval = (int)dr["UpdateInterval"];
                tmpBlog.Categories = new CategoriesCollection(dr["Categories"].ToString());
                tmpList.Add(tmpBlog);
            }
            return tmpList;
        }
        internal static List<Jiyuu.Aggregation.Common.Data.Blog> GetBlogs(System.Data.DataSet dataSet)
        {
            if (dataSet.Tables.Count <= 0)
                return new List<Jiyuu.Aggregation.Common.Data.Blog>();

            return (GetBlogs(dataSet.Tables[0]));
        }
    }
}
