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
        if (allFilled)
        {
            hotel newHot = hotel.add(usernameTXT.Text, passTXT.Text, hnameTXT.Text, addTXT.Text, cityTXT.Text, stDRP.Text, phoneTXT.Text, Convert.ToInt32(zipTXT.Text));
            if (newHot == null /*Returns null if username already exists in the database*/)
            {
                errorTXT.Visible = true;
                errorTXT.Text = "Username already exists";
            }
            else
            {
                if (Response.IsClientConnected)
                {
                    Response.Redirect("~//LoginFormHot.aspx", true);
                }
                else
                {
                    errorTXT.Visible = true;
                    errorTXT.Text = "Lost Connection";
                    Response.End();
                }
            }
        }
        else
        {
            errorTXT.Visible = true;
            errorTXT.Text = "Please fill in all fields!";
        }
    }

    protected bool allFilled
    {
        get
        {
            if (addTXT.Text != string.Empty && cityTXT.Text != string.Empty && hnameTXT.Text != string.Empty && passTXT.Text != string.Empty && usernameTXT.Text != string.Empty && phoneTXT.Text != string.Empty && zipTXT.Text != string.Empty)
            {
                try
                {
                    int num = Convert.ToInt32(zipTXT.Text);
                    return true;
                }
                catch (InvalidCastException) { }
            }
            return false;
        }
    }
}