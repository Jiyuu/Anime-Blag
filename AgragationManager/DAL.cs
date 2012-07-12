using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Jiyuu.Aggregation.Common.Enums;
namespace Jiyuu.Aggregation
{

    static class DAL
    {
        internal static void LogFeedRequest(long blogID,FeedReqTypeEnum reqType,  string xmlRssRequest)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("LogFeedRequest");
            db.AddInParameter(command, "@RssRequest", DbType.Xml, xmlRssRequest);
            db.AddInParameter(command, "@FeedReqType", DbType.Int32, (int)reqType);
            db.AddInParameter(command, "@BlogID", DbType.Int64, blogID);

            db.ExecuteNonQuery(command);
        }

        internal static DataSet GetAllBlogs()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command= db.GetStoredProcCommand("GetAllBlogs");
            return db.ExecuteDataSet(command);
        }

        //internal static DataSet GetAllPosts()
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand command = db.GetStoredProcCommand("GetAllPosts");
        //    return db.ExecuteDataSet(command);
        //}

        internal static DataSet GetPosts(long? blogID,string category)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("GetPosts");
            
            if (blogID.HasValue)
                db.AddInParameter(command, "@BlogID", DbType.Int64, blogID.Value);
            if (category!=null)
                db.AddInParameter(command, "@Category", DbType.String, category);


            return db.ExecuteDataSet(command);
        }

        internal static DataSet GetBlogs()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("GetBlogs");
            return db.ExecuteDataSet(command);
        }

        internal static void SaveBlogPost(string authorName, long blogID, string subject, string body, string summary, DateTime publicationTS, string link, string guid, string Categories)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("SaveBlogPost");
            db.AddInParameter(command, "@AuthorName", DbType.String, authorName);
            db.AddInParameter(command, "@BlogID", DbType.Int64, blogID);
            db.AddInParameter(command, "@Subject", DbType.String, subject);
            db.AddInParameter(command, "@Body", DbType.String, body);
            db.AddInParameter(command, "@Summary", DbType.String, summary);
            db.AddInParameter(command, "@PublicationTS", DbType.DateTime, publicationTS);
            db.AddInParameter(command, "@Link", DbType.String, link);
            db.AddInParameter(command, "@Guid", DbType.String, guid);
            db.AddInParameter(command, "@Categories", DbType.String, Categories);
            db.ExecuteNonQuery(command);
        }

    }
}
