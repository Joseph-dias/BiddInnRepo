using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    hotel thisHotel;
    protected void Page_Load(object sender, EventArgs e)
    {
        thisHotel = Session["User"] as hotel;
        Unit width = 10;
        BorderStyle border = BorderStyle.Ridge;
        List<DBConnector> offers = (new offerRetriever()).retrieve();
        TableRow topRow = new TableRow();
        TableCell custTTL = new TableCell();
        TableCell roomTTL = new TableCell();
        TableCell numTTL = new TableCell();
        TableCell sTTL = new TableCell();
        TableCell eTTL = new TableCell();
        TableCell totTTL = new TableCell();
        TableCell acceptTTL = new TableCell();
        topRow.ForeColor = System.Drawing.Color.Red;
        custTTL.Text = "Customer";
        roomTTL.Text = "Room Name";
        numTTL.Text = "Number of Rooms";
        sTTL.Text = "First Night";
        eTTL.Text = "Checkout Day";
        totTTL.Text = "Total Offer";
        acceptTTL.Text = "Accept";
        topRow.Cells.Add(custTTL);
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
                if(thisHotel.returnId == myOffer.offerHotel.returnId)
                {
                    TableRow newRow = new TableRow();
                    TableCell custCell = new TableCell();
                    LinkButton custBTN = new LinkButton();
                    custBTN.Text = myOffer.offerCust.getFullName;
                    custBTN.Click += (m, t) =>
                    {
                        Session["Customer"] = myOffer.offerCust;
                        Response.Redirect("~//custPGE.aspx");
                    };
                    custCell.HorizontalAlign = HorizontalAlign.Justify;
                    custCell.Controls.Add(custBTN);
                    newRow.Cells.Add(custCell);
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
                    if (myOffer.returnState == offer.state.NOTHING)
                    {
                        Button acceptBTN = new Button();
                        acceptBTN.Text = "ACCEPT";
                        acceptBTN.BackColor = System.Drawing.Color.DarkGreen;
                        acceptBTN.ForeColor = System.Drawing.Color.White;
                        acceptBTN.Click += (g, p) =>
                        {
                            myOffer.setState(offer.state.ACCEPTED);
                            emailSender.sender.send(myOffer.offerCust, "OFFER ACCEPTED", "Your offer to " + myOffer.offerHotel.getName + " has been accepted.");
                            checkOffers(offers, myOffer);
                            Response.Redirect("~//acceptOffers.aspx");
                        };
                        Button declineBTN = new Button();
                        declineBTN.Text = "DECLINE";
                        declineBTN.BackColor = System.Drawing.Color.DarkRed;
                        declineBTN.ForeColor = System.Drawing.Color.White;
                        declineBTN.Click += (g, p) =>
                        {
                            myOffer.setState(offer.state.DECLINED);
                            emailSender.sender.send(myOffer.offerCust, "OFFER DECLINED", "Your offer to " + myOffer.offerHotel.getName + " has been rejected.");
                            Response.Redirect("~//acceptOffers.aspx");
                        };
                        cell6.Controls.Add(acceptBTN);
                        cell6.Controls.Add(declineBTN);
                    }
                    else {
                        cell6.BorderWidth = width;
                        cell6.BorderStyle = border;
                        if (myOffer.returnState == offer.state.ACCEPTED)
                        {
                            cell6.ForeColor = System.Drawing.Color.DarkGreen;
                            cell6.BorderColor = System.Drawing.Color.DarkGreen;
                            LinkButton acceptedInfo = new LinkButton();
                            acceptedInfo.Text = "ACCEPTED";
                            acceptedInfo.Click += (g, a) =>
                            {
                                Session["offerUser"] = myOffer.offerCust;
                                Session["myOffer"] = myOffer;
                                Response.Redirect("~//offerInfo.aspx");
                            };
                            cell6.Controls.Add(acceptedInfo);
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

    /// <summary>
    /// Check for conflicting offers with recently accepted offer, and automatically decline them
    /// </summary>
    /// <param name="o">list of offers</param>
    /// <param name="a">recently accepted offer</param>
    protected void checkOffers(List<DBConnector> o, offer a) {
        if (a.returnState == offer.state.ACCEPTED)
        {
            foreach (DBConnector connect in o)
            {
                if (connect is offer)
                {
                    offer c = connect as offer;
                    if (c.returnState == offer.state.NOTHING) { //If state is not set on offer
                        if (a.offerRoom.getId == c.offerRoom.getId && c.active(a.fNight, a.checkout))
                        {
                            c.setState(offer.state.DECLINED); //Automatically decline if conflict is detected
                            emailSender.sender.send(c.offerCust, "OFFER DECLINED", "Your offer to " + c.offerHotel.getName + " has been rejected.");
                        }
                    }
                }
            }
        }
    }
}