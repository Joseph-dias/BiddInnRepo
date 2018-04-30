using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    hotel hot;
    protected void Page_Load(object sender, EventArgs e)
    {
        hot = Session["User"] as hotel;
    }

    protected void submitBTN_Click(object sender, EventArgs e) 
    {
        if (validate())
        {
            if (descTXT.Text == "")
            {
                hot.addRoom(pcUpload.FileBytes, Convert.ToDouble(priceTXT.Text), nameTXT.Text, Convert.ToInt32(numTXT.Text));
            }
            else {
                hot.addRoom(pcUpload.FileBytes, Convert.ToDouble(priceTXT.Text), nameTXT.Text, Convert.ToInt32(numTXT.Text), descTXT.Text);
            }
            Response.Redirect("~//HotPGE.aspx", true);
        }
        else {
            errorTXT.Visible = true;
            Response.Redirect("~//addRoom.aspx", true);
        }
    }

    protected bool validate()
    {
        try
        {
            double money = Convert.ToDouble(priceTXT.Text);
            int num = Convert.ToInt32(numTXT.Text);
            string fileName = pcUpload.FileName;
            if (!(Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".JPG") || money <= 0.0 || num <= 0 || nameTXT.Text == "") return false;
            return true;
        }
        catch (InvalidCastException) {
            return false;
        }
    }
}