using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class DefaultMaster : System.Web.UI.MasterPage
{
    public string PageTitle { get { return TitleLtr.Text; } set { TitleLtr.Text = value; } }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public string BrowserString()
    {
        if (Request.Browser.Browser == "IE" || Request.Browser.Browser == "Firefox")
            return "BT_" + Request.Browser.Browser.ToUpper();
        else
            return "BT_OTHER";
    }


}
