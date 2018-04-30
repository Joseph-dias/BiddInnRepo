using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    public string url = null;
    hotel hot;
    customer cust;
    protected Unit width = 10;
    protected BorderStyle border = BorderStyle.Solid;
    protected void Page_Load(object sender, EventArgs e)
    {
        cust = null;
        if (Session["User"] is hotel)
        {
            hot = Session["User"] as hotel;
            int outstanding = hot.outstandingOffers;
            if (outstanding > 0) {
                offerBTN.Text = outstanding.ToString();
                offerBTN.BackColor = Color.Green;
                offerBTN.ForeColor = Color.LightBlue;
            }
            rBTN.Visible = false;
        }
        else if(Session["User"] is customer) {
            hot = Session["Hotel"] as hotel; //Current user is not a hotel.
            logoutBTN.Visible = false;
            offerBTN.Visible = false;
            addBTN.Visible = false;
            FileUpload1.Visible = false;
            uploadBTN.Visible = false;
            cust = Session["User"] as customer;
            if (hot.hasReviewed(cust)) rBTN.Enabled = false;
        }
        //hot = Session["User"] as hotel;
        setIMG();
        setInfo();
        custNME.Text = hot.getName;
        List<review> theReviews = hot.myReviews;
        int num = 0;
        double addRating = 0;
        foreach (review r in theReviews) {
            num++;
            addRating += r.getRating;
            addReview(r);
        }
        if (num > 0) setAVGRating(addRating / num);
    }

    /// <summary>
    /// Add review to the table
    /// </summary>
    /// <param name="toAdd"></param>
    protected void addReview(review toAdd) {
        TableRow fRow = new TableRow();
        TableRow tRow = new TableRow();
        TableCell cell1 = new TableCell();
        cell1.BorderStyle = border;
        cell1.BorderWidth = width;
        TableCell cell2 = new TableCell();
        TableCell cell3 = new TableCell();
        cell3.BorderStyle = border;
        cell3.BorderWidth = width;
        if (Session["User"] is customer) cell1.Text = toAdd.rCustomer.getFullName; //Customer name
        else {
            LinkButton newBTN = new LinkButton();
            newBTN.Text = toAdd.rCustomer.getFullName;
            newBTN.Click += (e, a) =>
            {
                Session["Customer"] = toAdd.rCustomer;
                Response.Redirect("~//custPGE.aspx");
            };
            cell1.Controls.Add(newBTN);
        }
        double r = toAdd.getRating;
        Color t;
        if (r < 3) t = Color.PaleVioletRed;
        else t = Color.LightGreen;
        //cell2.ForeColor = Color.White;
        cell2.BackColor = t;
        cell2.BorderStyle = border;
        cell2.BorderWidth = width;
        cell2.HorizontalAlign = HorizontalAlign.Center;
        cell2.Font.Bold = true;
        cell2.Text = string.Format("{0:0.0}", r.ToString());
        fRow.Cells.Add(cell1);
        fRow.Cells.Add(cell2);
        reviewTBL.Rows.Add(fRow);
        cell3.Text = toAdd.getComments;
        cell3.ColumnSpan = 2; //Span 2 columns
        tRow.Cells.Add(cell3);
        reviewTBL.Rows.Add(tRow);
        TableRow paddRow = new TableRow();
        paddRow.Cells.Add(new TableCell());
        reviewTBL.Rows.Add(paddRow);
    }

    protected void setIMG()
    {
        System.Drawing.Image mainImage = hot.getMainPic;
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
        infoTXT.Text = "\n\n" + hot.fullAddress;
    }

    protected void setAVGRating(double rating) {
        avgLBL.Visible = true;
        avgTXT.Visible = true;
        avgTXT.Text = string.Format("{0:0.0}", rating);
        if (rating < 3) avgTXT.ForeColor = Color.OrangeRed;
        else if (rating >= 4) avgTXT.ForeColor = Color.LightGreen;
    }

    protected void uploadBTN_Click(object sender, EventArgs e)
    {
        string ex = Path.GetExtension(FileUpload1.FileName);
        if (ex == ".jpg" || ex == ".JPG")
        {
            hot.setMainPic = FileUpload1.FileBytes;
            setIMG();
        }
    }

    protected void logoutBTN_Click(object sender, EventArgs e)
    {
        Session["User"] = null;
        Response.Redirect("~//Homepage.aspx");
    }

    protected void offerBTN_Click(object sender, EventArgs e)
    {
        Response.Redirect("~//acceptOffers.aspx");
    }

    protected void rBTN_Click(object sender, EventArgs e)
    {
        Session["Review Customer"] = cust;
        Session["Review Hotel"] = hot;
        Response.Redirect("~//newReview.aspx");
    }
}