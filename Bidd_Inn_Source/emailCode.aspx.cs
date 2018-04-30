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
        if (!Page.IsPostBack)
        {
            customer email = Session["Email"] as customer;
            Random newRand = new Random();
            int code = newRand.Next(11111, 99999);
            Session["Code"] = code;
            emailSender.sender.send(email, "New Email", "Someone requested to change your password.\n\nYour secret code is: " + code.ToString() + "\nfor\nUsername: " + email.username + "\n\nIf this wasn't you, disregard this message.  Otherwise, enter the code into the window and set a new password.");
        }
    }

    protected void smtBTN_Click(object sender, EventArgs e)
    {
        if (Session["Code"].ToString() == codeTXT.Text && passTXT.Text != "") {
            (Session["Email"] as customer).newPassword = passTXT.Text;
        }
        Session["Code"] = null;
        Session["Email"] = null;
        Response.Redirect("~//Homepage.aspx");
    }
}