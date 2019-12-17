using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.User
{
    public partial class UserShared : System.Web.UI.MasterPage
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Customer customer = (Customer)Session["Customer"];

                if (customer != null)
                {
                    CustomerNameLabel.Text = customer.CustomerName;
                }
            }
        }

        protected void Userlogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/default.aspx", true);
        }
    }
}