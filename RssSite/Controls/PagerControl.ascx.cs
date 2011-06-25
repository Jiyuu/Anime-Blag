using System;
using System.Collections;
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

public partial class Controls_PagerControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected string PrevLink { get; set; }
    protected string NextLink { get; set; }
    public void SetPaging(int pagesCount, int currentPage)
    { 
        currentPage++;
        if (pagesCount > 1)
        {

            if (currentPage < pagesCount)
            {
                NextLink = (currentPage + 1).ToString();
            }
            else
                NextLink = "";

            if (currentPage > 1)
            {
                PrevLink = (currentPage - 1).ToString();
            }
            else
                PrevLink = "";

            for (int i = 1; i <= pagesCount; i++)
            {
                PostPagingDdl.Items.Add(i.ToString());
            }
            try
            {
                PostPagingDdl.SelectedValue = currentPage.ToString();
            }
            catch { }
        }
        else
            PagerHolder.Style["display"] = "none";
    }
}
