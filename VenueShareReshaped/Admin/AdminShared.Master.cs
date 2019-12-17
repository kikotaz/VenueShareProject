using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    public partial class AdminShared : System.Web.UI.MasterPage
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            //Check if there is authenticated Admin logged in
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                AdminObject admin = (AdminObject)Session["Admin"];

                if (admin != null)
                {
                    adminName.Text = admin.Name;
                }
                else
                {
                    //Creating Identity for Admin to retrieve ticket
                    FormsIdentity adminIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                    FormsAuthenticationTicket ticket = adminIdentity.Ticket;

                    //If the ticket not expired, creating new session
                    if (!ticket.Expired)
                    {
                        //Building read query
                        string query = "select * from tblAdmins where Email='" + ticket.Name +
                            "'";

                        //Getting reader result 
                        var reader = dbCommander.ReadRecord(query);

                        if (reader != null)
                        {
                            AdminObject requestedAdmin = new AdminObject();

                            while (reader.Read())
                            {
                                requestedAdmin.AdminID = (Guid)reader["AdminID"];
                                requestedAdmin.Name = (String)reader["Name"];
                                requestedAdmin.Email = (String)reader["Email"];
                            }


                            //Closing connection
                            dbCommander.CloseConnection();

                            Session["Admin"] = requestedAdmin;
                            adminName.Text = requestedAdmin.Name;
                        }
                        else
                        {
                            dbCommander.CloseConnection();
                        }
                    }

                }
            }
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/Admin/AdminLogin.aspx", true);
        }
    }
}