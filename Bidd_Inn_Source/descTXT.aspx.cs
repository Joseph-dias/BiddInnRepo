using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    room r;
    protected void Page_Load(object sender, EventArgs e)
    {
        r = Session["Desc"] as room;
        if (!Page.IsPostBack)
        {
            descTXT.Text = r.description;
        }
    }

    protected void saveBTN_Click(object sender, EventArgs e)
    {
        r.description = descTXT.Text;
        Session["Desc"] = null;
        Response.Redirect("~//Homepage.aspx");
    }
}