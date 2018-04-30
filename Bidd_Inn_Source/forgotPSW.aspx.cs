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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (emailBX.Text != "") {
            List<DBConnector> custs = (new customerRetriever()).retrieve();
            foreach (DBConnector connect in custs) {
                if (connect is customer) {
                    if ((connect as customer).getEmail == emailBX.Text) {
                        Session["Email"] = connect as customer;
                        Response.Redirect("~//emailCode.aspx");
                    }
                }
            }
        }
    }
}