using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    customer cust;
    hotel hot;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cust = Session["Review Customer"] as customer;
            hot = Session["Review Hotel"] as hotel;
        }
        catch (Exception) {
            Response.Redirect("~//Homepage.aspx");
        }
    }

    protected void cBTN_Click(object sender, EventArgs e)
    {
        cust = Session["Review Customer"] as customer;
        hot = Session["Review Hotel"] as hotel;
        hot.addReview(Convert.ToDouble(starBTN.SelectedValue), comTXT.Text, cust);
        Session["Hotel"] = hot;
        Session["Review Customer"] = null;
        Session["Review Hotel"] = null;
        Response.Redirect("~//HotPGE.aspx");
    }
}