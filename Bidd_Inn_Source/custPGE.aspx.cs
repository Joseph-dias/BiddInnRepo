using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string url = null;
    customer cust;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] is customer)
        {
            cust = Session["User"] as customer;
            offerBTN.Text = cust.numOffersSent[0].ToString(); //Put number on offer button
            if (Convert.ToInt32(offerBTN.Text) > 0)
            {
                offerBTN.BackColor = System.Drawing.Color.Green;
                offerBTN.ForeColor = System.Drawing.Color.Yellow;
            }
            else if (Convert.ToInt32(offerBTN.Text) < 0) offerBTN.Text = "0";
        }
        else
        {
            cust = Session["Customer"] as customer;
            offerBTN.Visible = false;
            logBTN.Visible = false;
            FileUpload1.Visible = false;
            uploadBTN.Visible = false;
        }
        setIMG();
        setInfo();
        custNME.Text = cust.getFullName;
    }

    protected void setIMG()
    {
        System.Drawing.Image mainImage = cust.getMainPic;
        if (mainImage != null)
        {
            url = "~/TempImages/" + Path.GetRandomFileName();
            url = Path.ChangeExtension(url, ".jpg");
            mainImage.Save(Server.MapPath(url));
            mainIMG.ImageUrl = url;
            UpdatePicPNL.Update();
        }
    }

    protected void UpdatePicPNL_Unload(object sender, EventArgs e)
    {
        //if (url != null) File.Delete(Server.MapPath(url));
    }

    protected void setInfo() {
        infoTXT.Text = "AGE: " + cust.yearsOld.ToString() + "\n\nHOME CITY: " + cust.homeCity;
    }

    protected void uploadBTN_Click(object sender, EventArgs e)
    {
        string ex = Path.GetExtension(FileUpload1.FileName);
        if (ex == ".jpg" || ex == ".JPG")
        {
            cust.setMainPic = FileUpload1.FileBytes;
            setIMG();
        }
    }

    protected void logBTN_Click(object sender, EventArgs e)
    {
        Session["User"] = null;
        Response.Redirect("~//Homepage.aspx");
    }

    protected void offerBTN_Click(object sender, EventArgs e)
    {
        (Session["User"] as customer).addSeen(Convert.ToInt32(offerBTN.Text));
        Response.Redirect("~//seeOffers.aspx");
    }
}