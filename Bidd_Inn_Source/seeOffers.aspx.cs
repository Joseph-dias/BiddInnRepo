using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    customer thisCust;
    protected void Page_Load(object sender, EventArgs e)
    {
        thisCust = Session["User"] as customer;
        Unit width = 10;
        BorderStyle border = BorderStyle.Ridge;
        List<DBConnector> offers = (new offerRetriever()).retrieve();
        TableRow topRow = new TableRow();
        TableCell hotTTL = new TableCell();
        TableCell roomTTL = new TableCell();
        TableCell numTTL = new TableCell();
        TableCell sTTL = new TableCell();
        TableCell eTTL = new TableCell();
        TableCell totTTL = new TableCell();
        TableCell acceptTTL = new TableCell();
        topRow.ForeColor = System.Drawing.Color.Red;
        hotTTL.Text = "Hotel";
        roomTTL.Text = "Room Name";
        numTTL.Text = "Number of Rooms";
        sTTL.Text = "First Night";
        eTTL.Text = "Checkout Day";
        totTTL.Text = "Total Offer";
        acceptTTL.Text = "Accepted";
        topRow.Cells.Add(hotTTL);
        topRow.Cells.Add(roomTTL);
        topRow.Cells.Add(numTTL);
        topRow.Cells.Add(sTTL);
        topRow.Cells.Add(eTTL);
        topRow.Cells.Add(totTTL);
        topRow.Cells.Add(acceptTTL);
        topRow.BorderWidth = width;
        topRow.BorderStyle = border;
        offerTBL.Rows.Add(topRow);

        foreach (DBConnector theOffer in offers)
        {
            if (theOffer is offer)
            {
                offer myOffer = theOffer as offer;
                if(thisCust.returnId == myOffer.offerCust.returnId)
                {
                    TableRow newRow = new TableRow();
                    TableCell hotCell = new TableCell();
                    LinkButton hotBTN = new LinkButton();
                    hotBTN.Text = myOffer.offerHotel.getName;
                    hotBTN.Click += (m, t) =>
                    {
                        Session["Hotel"] = myOffer.offerHotel;
                        Response.Redirect("~//custPGE.aspx");
                    };
                    hotCell.HorizontalAlign = HorizontalAlign.Justify;
                    hotCell.Controls.Add(hotBTN);
                    newRow.Cells.Add(hotCell);
                    TableCell cell1 = new TableCell();
                    LinkButton button1 = new LinkButton();
                    button1.Text = myOffer.offerRoom.ToString();
                    button1.Click += (s, a) => {
                        Session["Room"] = myOffer.offerRoom;
                        Response.Redirect("~//rmPGE.aspx");
                    };
                    cell1.HorizontalAlign = HorizontalAlign.Justify;
                    cell1.Controls.Add(button1);
                    newRow.Cells.Add(cell1);
                    TableCell cell2 = new TableCell();
                    cell2.Text = myOffer.getNumRooms.ToString();
                    cell2.HorizontalAlign = HorizontalAlign.Left;
                    newRow.Cells.Add(cell2);
                    TableCell cell3 = new TableCell();
                    cell3.HorizontalAlign = HorizontalAlign.Left;
                    cell3.Text = myOffer.fNight.ToString("MM/dd/yy"); //Add first night to the table
                    newRow.Cells.Add(cell3);
                    TableCell cell4 = new TableCell();
                    cell4.HorizontalAlign = HorizontalAlign.Left;
                    cell4.Text = myOffer.checkout.ToString("MM/dd/yy");
                    newRow.Cells.Add(cell4);
                    TableCell cell5 = new TableCell();
                    cell5.HorizontalAlign = HorizontalAlign.Left;
                    cell5.Text = string.Format("${0:00}", myOffer.getTotal);
                    newRow.Cells.Add(cell5);
                    TableCell cell6 = new TableCell();
                    cell6.BorderWidth = width;
                    cell6.BorderStyle = border;
                    if (myOffer.returnState == offer.state.NOTHING)
                    {
                        cell6.BackColor = System.Drawing.Color.Gray;
                        cell6.ForeColor = System.Drawing.Color.Black;
                        cell6.Text = "PENDING";
                    }
                    else {
                        if (myOffer.returnState == offer.state.ACCEPTED)
                        {
                            cell6.ForeColor = System.Drawing.Color.DarkGreen;
                            cell6.BorderColor = System.Drawing.Color.DarkGreen;
                            LinkButton acceptedBTN = new LinkButton();
                            acceptedBTN.Text = "ACCEPTED";
                            acceptedBTN.Click += (g, p) =>
                            {
                                Session["offerUser"] = myOffer.offerHotel;
                                Session["myOffer"] = myOffer;
                                Response.Redirect("~//offerInfo.aspx");
                            };
                            cell6.Controls.Add(acceptedBTN);
                        }
                        else {
                            cell6.ForeColor = System.Drawing.Color.DarkRed;
                            cell6.BorderColor = System.Drawing.Color.DarkRed;
                            cell6.Text = "DECLINED";
                        }
                    }
                    newRow.Cells.Add(cell6);
                    newRow.BorderWidth = width;
                    newRow.BorderStyle = border;
                    offerTBL.Rows.Add(newRow);
                }
            }
        }
    }
}