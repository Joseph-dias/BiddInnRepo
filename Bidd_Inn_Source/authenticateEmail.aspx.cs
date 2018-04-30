using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            customer cust = Session["User"] as customer;
            if (cust.emailAuthenticated) Response.Redirect("~//custPGE.aspx");
            else
            {
                Session["Code"] = emailSender.sender.authenticate(cust);
            }
        }
    }

    protected void authenticateBTN_Click(object sender, EventArgs e)
    {
        if (Session["Code"].ToString() == inputTXT.Text) {
            (Session["User"] as customer).authenticateEmail();
            Response.Redirect("~//custPGE.aspx");
        }
    }
}