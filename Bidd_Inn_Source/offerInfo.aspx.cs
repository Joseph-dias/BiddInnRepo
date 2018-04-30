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
        siteUser theUser = Session["offerUser"] as siteUser;
        offer theOffer = Session["myOffer"] as offer;
        nmeBTN.Text = theUser.outputName();
        infoTXT.Text = theOffer.ToString() + "\n\n" + theUser.outputInfo();
        roomTXT.Text = theOffer.offerRoom.ToString();
    }

    protected void roomTXT_Click(object sender, EventArgs e)
    {
        Session["Room"] = (Session["myOffer"] as offer).offerRoom;
        Response.Redirect("~//rmPGE.aspx");
    }

    protected void nmeBTN_Click(object sender, EventArgs e)
    {
        siteUser theUser = Session["offerUser"] as siteUser;
        if(theUser is customer)
        {
            Session["Customer"] = theUser;
            Response.Redirect("~//custPGE.aspx");
        }else
        {
            Session["Hotel"] = theUser;
            Response.Redirect("~//HotPGE.aspx");
        }
    }
}