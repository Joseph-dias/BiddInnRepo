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
        for (int x = 1; x <= 31; x++) {
            dDRP.Items.Add(x.ToString());
        }
        for (int x = 18; x <= 100; x++) {
             yDRP.Items.Add(DateTime.Now.AddYears(-x).Year.ToString());
        }
    }

    protected void sbmtBTN_Click(object sender, EventArgs e)
    {
        DateTime newTime;
        if (allFilled && DateTime.TryParse(mDRP.SelectedValue + "/" + dDRP.SelectedValue + "/" + yDRP.SelectedValue, out newTime))
        {
            customer checkCust = customer.add(usernameTXT.Text, passTXT.Text, fnameTXT.Text, lnameTXT.Text, addTXT.Text, cityTXT.Text, stDRP.Text, emailTXT.Text, newTime, Convert.ToInt32(zipTXT.Text));
            if (checkCust == null /*Returns null if username already exists in the database*/)
            {
                errorTXT.Visible = true;
                errorTXT.Text = "Username already exists";
            }
            else
            {
                if (Response.IsClientConnected)
                {
                    Response.Redirect("~//LoginFormCust.aspx", true);
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

    protected bool allFilled {
        get {
            if (addTXT.Text != string.Empty && cityTXT.Text != string.Empty && fnameTXT.Text != string.Empty && lnameTXT.Text != string.Empty && passTXT.Text != string.Empty && usernameTXT.Text != string.Empty && emailTXT.Text != string.Empty && mDRP.SelectedValue != "0" && dDRP.SelectedIndex != 0 && yDRP.SelectedIndex != 0 && zipTXT.Text != string.Empty) {
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