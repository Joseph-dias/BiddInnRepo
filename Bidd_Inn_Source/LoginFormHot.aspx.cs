using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void sbmtBTN_Click(object sender, EventArgs e)
    {
        if (userTXT.Text != "" && passTXT.Text != "")
        {
            try
            {
                hotel hot = new hotel(userTXT.Text, passTXT.Text);
                Session.Add("User", hot);
                Response.Redirect("~//HotPGE.aspx");
            }
            catch (Exception)
            {
                notFDLBL.Visible = true;
            }
        }
        else notFDLBL.Visible = true;
    }
}