using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Timers;

public partial class _Default : Page
{
    private System.Timers.Timer timer;
    private retriever myR;
    public string hotelNum { get; set; }
    public string custNum { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        //customer cust = new customer("Stash", "password");
        //System.Drawing.Image testImage = cust.getMainPic;
        //url = "~/TempImages/" + Path.GetRandomFileName();   //SAVE THIS CODE FOR LOADING PICTURES
        //url = Path.ChangeExtension(url, ".jpg");
        //testImage.Save(Server.MapPath(url));
        //Image1.ImageUrl = url;
        //updatePNL.Update();
        //Session["User"] = null;
        if (Session["User"] is hotel) Response.Redirect("~//HotPGE.aspx");
        else if (Session["User"] is customer) Response.Redirect("~//authenticateEmail.aspx");
        setNumVars();
        cNum.Text = custNum;
        hNum.Text = hotelNum;
        timer = new System.Timers.Timer(10000);
        timer.Elapsed += timer_tick;
        timer.Start();
    }

    private void timer_tick(object sender, EventArgs e)
    {
        setNumVars();
    }

    private void setNumVars()
    {
        myR = new hotelRetriever();
        hotelNum = myR.retrieve().Count.ToString();
        myR = new customerRetriever();
        custNum = myR.retrieve().Count.ToString();
    }

    private string mapURL(string path) {
        string thePath = Server.MapPath("/").ToLower();
        return string.Format("/{0}", path.ToLower().Replace(thePath, "").Replace(@"\", "/"));
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void enterBTN_Click(object sender, EventArgs e)
    {
        try
        {
            //User newCust = customer.add("stash", "password", "Joey", "Dias", "2765 champagne lane", "Tillamook", "Oregon", "stash5000@gmail.com");
            try
            {
                customer cust = new customer("Stash", "password");
                //if (fileUpload.HasFile) {
                    /*string fileName = Path.GetFileName(fileUpload.FileName);
                    cust.setMainPic = fileUpload.FileBytes;*/
                    System.Drawing.Image testImage = cust.getMainPic;
                    string tempFile = Path.GetTempPath();
                    int x = 1;
                    while (File.Exists(tempFile + x + "imgTMP.jpeg")) { x++; }
                    testImage.Save(tempFile + x + "imgTMP.jpeg");
                    //imageFLD.ImageUrl = tempFile + x + "imgTMP.jpeg";
                    Response.Redirect("~\\Homepage.aspx", false);
                //}
                
            }
            catch (Exception t) { /*ADD "USER NOT FOUND" TEXT*/ }
        }
        catch (Exception l) {
            //TODO: Add Error Text
        }
    }
}