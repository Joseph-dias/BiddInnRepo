using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected Unit width = 10;
    protected BorderStyle border = BorderStyle.Ridge;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Cities"] = new List<string>();
        if (!Page.IsPostBack && Session["Filter"] == null)
        {
            Session["fDate"] = null;
            Session["lDate"] = null;
            Session["Location"] = null;
        }
        else Session["Filter"] = null;
        if (!Page.IsPostBack)
        {
            locationDRP.Items.Clear();
            ListItem newItem = new ListItem("Location", "Location");
            newItem.Selected = true;
            locationDRP.Items.Add(newItem);
        }
        try
        {
            DateTime s = (DateTime)Session["fDate"];
            DateTime f = (DateTime)Session["lDate"];
            string loc = Session["Location"].ToString();
            populateList(s, f, loc);
            printDates(s, f, loc);
        }
        catch (Exception)
        {
            populateList();
            printDefaultDates(); //Print the default dates in the textboxes
        }
    }

    protected void printDates(DateTime s, DateTime f, string loc) {
        fDTXT.Text = s.Date.Day.ToString();
        fMTXT.Text = s.Date.Month.ToString();
        fYTXT.Text = s.Date.Year.ToString();
        lDTXT1.Text = f.Date.Day.ToString();
        lMTXT.Text = f.Date.Month.ToString();
        lYTXT.Text = f.Date.Year.ToString();
        //locationDRP.SelectedItem.Value = loc;
    }

    protected void printDefaultDates()
    {
        DateTime tomorrow = DateTime.Now.AddDays(1);
        fDTXT.Text = tomorrow.Day.ToString();
        fMTXT.Text = tomorrow.Month.ToString();
        fYTXT.Text = tomorrow.Year.ToString();
        DateTime nextDay = DateTime.Now.AddDays(2); //Get the next day
        lDTXT1.Text = nextDay.Day.ToString();
        lMTXT.Text = nextDay.Month.ToString();
        lYTXT.Text = nextDay.Year.ToString();
    }

    protected void populateList()
    {
        List<DBConnector> hotels = (new hotelRetriever()).retrieve();
        listTopper();


        foreach (DBConnector hot in hotels) {
            if (hot is hotel) {
                foreach (room rooms in (hot as hotel).myRooms)
                {
                    addRoom(rooms, hot as hotel);
                }
            }
        }
    }

    protected void populateList(DateTime s, DateTime f, string location) {
        List<DBConnector> hotels = (new hotelRetriever()).retrieve();
        listTopper();


        foreach (DBConnector hot in hotels)
        {
            if (hot is hotel)
            {
                foreach (room rooms in (hot as hotel).myRooms)
                {
                    if (rooms.numAvailable(s, f) >= 1 && rooms.roomHotel.cityState == location)  //Only if room is available on the given dates and in the given location
                    {
                        addRoom(rooms, hot as hotel, location);
                    }else if(!((List<string>)Session["Cities"]).Contains(rooms.roomHotel.cityState))
                    {
                        ListItem newItem = new ListItem(rooms.roomHotel.cityState, rooms.roomHotel.cityState);
                        locationDRP.Items.Add(newItem);
                        ((List<string>)Session["Cities"]).Add(rooms.roomHotel.cityState);
                    }
                }
            }
        }
    }

    protected void listTopper() {
        roomTBL.Rows.Clear();
        TableRow topRow = new TableRow();
        TableCell picTTL = new TableCell();
        TableCell roomNME = new TableCell();
        TableCell hotNME = new TableCell();
        TableCell askedPrice = new TableCell();
        topRow.ForeColor = System.Drawing.Color.Red;
        picTTL.Text = "Picture";
        roomNME.Text = "Room Name";
        hotNME.Text = "Hotel";
        askedPrice.Text = "Asked Price";
        topRow.Cells.Add(picTTL);
        topRow.Cells.Add(roomNME);
        topRow.Cells.Add(hotNME);
        topRow.Cells.Add(askedPrice);
        topRow.BorderWidth = width;
        topRow.BorderStyle = border;
        roomTBL.Rows.Add(topRow);
    }

    protected void addRoom(room rooms, hotel hot, string loc = null) {
        TableRow newRow = new TableRow();
        TableCell cell1 = new TableCell();
        Image newIMG = new Image();
        newIMG.BorderStyle = BorderStyle.Solid;
        newIMG.Height = 150;
        newIMG.Width = 150;
        //Set img url
        System.Drawing.Image mainImage = rooms.getMainPic;
        string url;
        if (mainImage != null)
        {
            url = "~/TempImages/" + Path.GetRandomFileName();
            url = Path.ChangeExtension(url, ".jpg");
            mainImage.Save(Server.MapPath(url));
            newIMG.ImageUrl = url;
        }
        cell1.HorizontalAlign = HorizontalAlign.Justify;
        cell1.Controls.Add(newIMG);
        newRow.Cells.Add(cell1);  //Adding image control to row
        TableCell cell2 = new TableCell();
        if (Session["User"] is siteUser)
        {
            LinkButton button = new LinkButton();
            button.Text = rooms.ToString();
            button.Click += (s, e) =>
            {
                Session["Room"] = rooms;
                Response.Redirect("~//rmPGE.aspx");
            };
            cell2.HorizontalAlign = HorizontalAlign.Left;
            cell2.Controls.Add(button);
        }
        else cell2.Text = rooms.ToString();
        newRow.Cells.Add(cell2);
        TableCell cell3 = new TableCell();
        cell3.HorizontalAlign = HorizontalAlign.Left;
        if (!(Session["User"] is customer)) cell3.Text = hot.getName; //No link unless the user is a customer
        else {  //The user is a customer
            LinkButton newBTN = new LinkButton();
            newBTN.Text = hot.getName;
            newBTN.Click += (a, t) =>
            {
                Session["Hotel"] = hot;
                Response.Redirect("~//HotPGE.aspx");
            };
            cell3.Controls.Add(newBTN);
        }
        newRow.Cells.Add(cell3);
        TableCell cell4 = new TableCell();
        cell4.HorizontalAlign = HorizontalAlign.Left;
        cell4.Text = string.Format("${0:00}", rooms.getAskedPrice);
        newRow.Cells.Add(cell4);
        newRow.BorderWidth = width;
        newRow.BorderStyle = border;
        roomTBL.Rows.Add(newRow);
        if (!Page.IsPostBack)
        {
            List<string> myList = (List<string>)Session["Cities"];
            if (!(myList.Contains(hot.cityState)))
            {
                ListItem i = new ListItem(hot.cityState, hot.cityState); //Same for both text and value
                if (loc == hot.cityState)
                {
                    locationDRP.SelectedItem.Selected = false;
                    i.Selected = true;
                }
                locationDRP.Items.Add(i);
                myList.Add(hot.cityState);
                Session["Cities"] = myList;
            }
        }
    }

    protected void filterBTN_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            DateTime[] dates = validateFilter();
            if (dates != null)
            {
                if (errorTXT.Visible) errorTXT.Visible = false;
                Session["fDate"] = dates[0];
                Session["lDate"] = dates[1];
                Session["Location"] = locationDRP.SelectedItem.Value;
                Session["Filter"] = true;
                Response.Redirect("~//roomDisp.aspx");
            }
            else
            {
                errorTXT.Visible = true;
            }
        }
    }

    protected DateTime[] validateFilter() {
        if (fMTXT.Text != "" && fDTXT.Text != "" && fYTXT.Text != "" && lDTXT1.Text != "" && lMTXT.Text != "" && lYTXT.Text != "" && locationDRP.SelectedItem.Value != "Location")
        {
            try
            {
                DateTime sTime = new DateTime(Convert.ToInt32(fYTXT.Text), Convert.ToInt32(fMTXT.Text), Convert.ToInt32(fDTXT.Text));
                DateTime fTime = new DateTime(Convert.ToInt32(lYTXT.Text), Convert.ToInt32(lMTXT.Text), Convert.ToInt32(lDTXT1.Text));
                if ((DateTime.Now < sTime) && (sTime < fTime)) return new DateTime[2] {
                    sTime,
                    fTime
                };
            } catch (Exception) { }
        }
        return null;
    }

    protected void resetBTN_Click(object sender, EventArgs e)
    {
        if (errorTXT.Visible) errorTXT.Visible = false;
        Session["fDate"] = null;
        Session["lDate"] = null;
        Response.Redirect("~//roomDisp.aspx");
    }

    protected void locationDRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        //locationDRP.SelectedValue = locationDRP.SelectedItem.Value;
    }
}