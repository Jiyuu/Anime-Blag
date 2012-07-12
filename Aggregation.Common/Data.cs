using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jiyuu.Aggregation.Common.Enums;
using Jiyuu.Aggregation.Common.SharedData;

namespace Jiyuu.Aggregation.Common.Data.Lite
{
	public class BlogPost
	{
		public string Content { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Link { get; set; }
		public string Guid { get; set; }
		public DateTime PublicationTS { get; set; }
		public string PostAuthor {get;set;}
		public long Blog { get; set; }
		public CategoriesCollection Categories { get; set; }
        public int Comments { get; set; }
    }

    public class BlogComment
    {
        public long CommentID { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublicationTS { get; set; }
        public long Blog { get; set; }
    }

	public class BlogAuthor
	{
		public long AuthorID { get; set; }
		public string AuthorName { get; set; }
	}
	public class Blog
	{
		public long BlogID { get; set; }
		public string BlogName { get; set; }
        public string FeedURL { get; set; }
        public FeedTypeEnum FeedType { get; set; }
		public DateTime LastUpdateTS { get; set; }
		public int UpdateInterval { get; set; }
	}
	
}

namespace Jiyuu.Aggregation.Common.Data
{
	public class BlogPost
	{
		public long PostID {get; set; }
		public string Content { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Link { get; set; }
		public string Guid { get; set; }
		public DateTime PublicationTS { get; set; }
		public BlogAuthor PostAuthor { get; set; }
		public Blog Blog { get; set; }
		public CategoriesCollection Categories { get; set; }
        public int Comments { get; set; }
    }

    public class BlogComment
    {
        public long CommentID { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublicationTS { get; set; }
        public Blog Blog { get; set; }
    }

	public class BlogAuthor
	{
		public long AuthorID { get; set; }
		public string AuthorName { get; set; }
		public BlogAuthor (string authorName)
		{
				AuthorName=authorName;
		}
	}
	public class Blog
	{
		public bool IsActive { get; set; }
		public long BlogID { get; set; }
		public string BlogName { get; set; }
		public string FeedURL { get; set; }
		public FeedTypeEnum FeedType { get; set; }
		public string HomepageURL { get; set; }
		public DateTime LastUpdateTS { get; set; }
		public int UpdateInterval { get; set; }
		public CategoriesCollection Categories { get; set; }
        public string CommentsFeedURL { get; set; }
    }
}

namespace Jiyuu.Aggregation.Common.SharedData
{
	public class CategoriesCollection
	{   
		private static string delimiter = ";";
		private static string[] delimiterArr = new string[] { delimiter };
		public List<string> CategoriesList { get; set; }
		public string ToDelimited()
		{
			return String.Join(delimiter, CategoriesList.Select(s=>System.Web.HttpUtility.HtmlEncode(s)).ToArray());
		}
		public CategoriesCollection() { this.CategoriesList = new List<string>(); }

		public CategoriesCollection(string delimitedCategories)
		{
			CategoriesList = System.Web.HttpUtility.HtmlDecode(delimitedCategories).Split(delimiterArr, StringSplitOptions.RemoveEmptyEntries).ToList();
		}

		public void AddCategory(string category)
		{
			this.CategoriesList.Add(category);
		}
	}
}