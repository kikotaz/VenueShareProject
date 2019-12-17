using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    /*
     * @ClassName: AdminLogin
     * @Description: This class will be the code behind for AdminLogin.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class AdminLogin : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if there is authenticated Admin logged in
            if (HttpContext.Current.User.Identity.IsAuthenticated)
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

                        Session["Admin"] = requestedAdmin;

                        //Closing connection
                        dbCommander.CloseConnection();

                        //Redirecting to AdminManagement Page
                        Response.Redirect("/Admin/AdminManagement.aspx");
                    }
                }
            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Hashing the given password by admin for comparison
                PasswordHasher hasher = new PasswordHasher();
                string hashedPassword = hasher.GetHashedPassword(passwordInput.Text, emailInput.Text);

                //Building read query
                string query = "select * from tblAdmins where Email='" + emailInput.Text +
                    "' and PasswordHash='" + hashedPassword + "'";

                //Getting reader result 
                var reader = dbCommander.ReadRecord(query);

                //Check if the reader has returned rows or not
                if (reader.HasRows)
                {
                    AdminObject requestedAdmin = new AdminObject();
                    while (reader.Read())
                    {
                        requestedAdmin.Name = (String)reader["Name"];
                        requestedAdmin.Email = (String)reader["Email"];
                        requestedAdmin.AdminID = (Guid)reader["AdminID"];
                    }
                    
                    //Admin info is added to the session
                    Session["Admin"] = requestedAdmin;

                    //Create Authentication ticket
                    FormsAuthenticationTicket ticket;
                    string cookieInfo;
                    HttpCookie adminCookie;

                    //Stay logged in ticket
                    if (stayLogged.Checked)
                    {
                        ticket = new FormsAuthenticationTicket(requestedAdmin.Email, true, 60);
                    }
                    //Not to stay logged in ticket
                    else
                    {
                        ticket = new FormsAuthenticationTicket(requestedAdmin.Email, false, 1);
                    }

                    //Adding the authentication ticket to a cookie
                    cookieInfo = FormsAuthentication.Encrypt(ticket);
                    adminCookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieInfo);
                    adminCookie.Expires = ticket.Expiration;
                    adminCookie.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(adminCookie);

                    //Closing database connection
                    dbCommander.CloseConnection();

                    //Redirecting to AdminManagement page
                    Response.Redirect("/Admin/AdminManagement.aspx");
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException)
            {
                //If the the credentials are not correct, show error
                Response.Write
                    ("<script>alert('Wrong credentials, please check your provided data to be correct');</script>");

                //Closing connection after exception
                dbCommander.CloseConnection();
            }
        }
    }
}