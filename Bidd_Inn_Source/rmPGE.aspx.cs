using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    const string NO_ROOMS = "No rooms available";
    room thisRm;
    string url;
    siteUser theUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        thisRm = Session["Room"] as room;
        System.Drawing.Image mainImage = thisRm.getMainPic;
        roomNMELBL.Text = thisRm.ToString();
        url = "~/TempImages/" + Path.GetRandomFileName();
        url = Path.ChangeExtension(url, ".jpg");
        mainImage.Save(Server.MapPath(url));
        rmPC.ImageUrl = url;
        descTXT.Text = thisRm.description;
        priceLBL.Text = "$" + string.Format("{0:0.00}", thisRm.getAskedPrice.ToString());
        if (Session["User"] is customer)
        {
            LinkButton newBtn = new LinkButton();
            newBtn.Text = "(" + thisRm.roomHotel.getName + ")";
            newBtn.Click += (a, t) =>
            {
                Session["Hotel"] = thisRm.roomHotel;
                Response.Redirect("~//HotPGE.aspx");
            };
            hotelLBL.Text = string.Empty;
            hotelLBL.Controls.Add(newBtn);
            theUser = Session["User"] as customer;
            setupCustomer();
        }
        else if (Session["User"] is hotel)
        {
            hotelLBL.Text = "(" + thisRm.roomHotel.getName + ")";
            theUser = Session["User"] as hotel;
            setupHotel((theUser as hotel).returnId == thisRm.roomHotel.returnId);
        }
        else {
            subtractOffers();
        }
    }

    protected void submitBTN_Click(object sender, EventArgs e)
    {
        if (validOffer())
        {
            offer theOffer = new offer(thisRm.roomHotel, thisRm, Session["User"] as customer, Convert.ToInt32(numDRP.SelectedItem.Text), fCAL.SelectedDate, lCAL.SelectedDate, Convert.ToDouble(offerPRC.Text));
            Response.Redirect("~//roomDisp.aspx");
        }
        else {
            errorTXT.Visible = true;
        }
    }

    protected bool validOffer() {
        try
        {
            if (fCAL.SelectedDate != null && lCAL.SelectedDate != null && offerPRC.Text != "" && Session["User"] is customer && numDRP.SelectedItem.Text != NO_ROOMS)
            {
                double dollar = Convert.ToDouble(offerPRC.Text);
                if(lCAL.SelectedDate > fCAL.SelectedDate && fCAL.SelectedDate > DateTime.Now && dollar > 0) return true; 
            }
        }
        catch (FormatException) { }
        return false;
    }

    /// <summary>
    /// Setup the page for hotel view
    /// </summary>
    /// <param name="thisHot">Is this room owned by this hotel?</param>
    protected void setupHotel(bool thisHot) {
        if (thisHot) {
            editBTN.Visible = true;
        }
        subtractOffers();
    }

    protected void setupCustomer() {
        if (!Page.IsPostBack)
        {
            numDRP.Items.Clear();
            for (int x = 1; x <= thisRm.num; x++)
            { //Populating the number of rooms list
                ListItem newItem = new ListItem();
                newItem.Text = x.ToString();
                newItem.Value = x.ToString();
                numDRP.Items.Add(newItem);
            }
        }
    }

    /// <summary>
    /// Take the form off the page that allows the submission of offers
    /// </summary>
    protected void subtractOffers() {
        offerLBL.Visible = false;
        fnLBL.Visible = false;
        fCAL.Visible = false;
        lCAL.Visible = false;
        lnLBL.Visible = false;
        offerPRCLBL.Visible = false;
        dollarLBL.Visible = false;
        offerPRC.Visible = false;
        numRMLBL.Visible = false;
        numDRP.Visible = false;
        submitBTN.Visible = false;
    }

    protected void fCAL_SelectionChanged(object sender, EventArgs e)
    {
        resetRoomNums();
    }

    protected void lCAL_SelectionChanged(object sender, EventArgs e)
    {
        resetRoomNums();
    }

    protected void resetRoomNums() {
        if (lCAL.SelectedDate != null && fCAL.SelectedDate != null) {
            numDRP.Items.Clear();
            int num = thisRm.numAvailable(lCAL.SelectedDate, fCAL.SelectedDate);
            if (num <= 0)
            {
                ListItem newItem = new ListItem();
                newItem.Text = NO_ROOMS;
                newItem.Value = NO_ROOMS;
                numDRP.Items.Add(newItem);
            }
            else
            {
                for (int x = 1; x <= num; x++)
                { //Populating the number of rooms list
                    ListItem newItem = new ListItem();
                    newItem.Text = x.ToString();
                    newItem.Value = x.ToString();
                    numDRP.Items.Add(newItem);
                }
            }
        }
    }

    protected void editBTN_Click(object sender, ImageClickEventArgs e)
    {
        Session["Desc"] = thisRm;
        Response.Redirect("~//descTXT.aspx");
    }
}