using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.User
{
    /*
     * @ClassName: LoginOrRegister
     * @Description: This class will be the code behind for LoginOrRegister.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class LoginOrRegister : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

            //Check if there is authenticated Admin logged in
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Creating Identity for Admin to retrieve ticket
                FormsIdentity customerIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = customerIdentity.Ticket;

                //If the ticket not expired, creating new session
                if (!ticket.Expired)
                {
                    //Building read query
                    string query = "select * from Customers where CustomerMail='" + ticket.Name +
                        "'";

                    //Getting reader result 
                    var reader = dbCommander.ReadRecord(query);

                    if (reader != null)
                    {
                        Customer customer = new Customer();

                        while (reader.Read())
                        {
                            customer.CustomerID = (Guid)reader["CustomerID"];
                            customer.CustomerName = (string)reader["CustomerName"];
                            customer.CustomerEmail = (string)reader["CustomerMail"];
                        }

                        Session["Customer"] = customer;

                        //Closing connection
                        dbCommander.CloseConnection();

                        //Redirecting to AdminManagement Page
                        Response.Redirect("../default.aspx");
                    }
                }
            }
        }

        /*
         * @Method: loginBtn_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of login customer button.
         * The customer data will be compared to the database and create a cookie for the user
         */
        protected void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Hashing the given password by admin for comparison
                PasswordHasher hasher = new PasswordHasher();
                string hashedPassword =
                    hasher.GetHashedPassword(LoginUserPasswordInput.Text, LoginUserEmailInput.Text);

                //Building read query
                string query = "select * from Customers where CustomerMail='" +
                    LoginUserEmailInput.Text + "' and HashedPassword='" + hashedPassword + "'";

                //Getting reader result 
                var reader = dbCommander.ReadRecord(query);

                //Check if the reader has returned rows or not
                if (reader.HasRows)
                {
                    Customer customer = new Customer();
                    while (reader.Read())
                    {
                        customer.CustomerID = (Guid)reader["CustomerID"];
                        customer.CustomerName = (string)reader["CustomerName"];
                        customer.CustomerEmail = (string)reader["CustomerMail"];
                    }

                    //Customer info is added to session
                    Session["Customer"] = customer;

                    //Create Authentication ticket
                    FormsAuthenticationTicket ticket;
                    string cookieInfo;
                    HttpCookie customerCookie;

                    ticket = new FormsAuthenticationTicket(customer.CustomerEmail, true, 60);

                    //Adding the authentication ticket to a cookie
                    cookieInfo = FormsAuthentication.Encrypt(ticket);
                    customerCookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieInfo);
                    customerCookie.Expires = ticket.Expiration;
                    customerCookie.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(customerCookie);

                    //Closing database connection
                    dbCommander.CloseConnection();

                    //Building success message script to redirect after success
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<script>alert('Successfully logged in');");
                    builder.Append("window.location.href = '");
                    builder.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                    builder.Append("/default.aspx");
                    builder.Append("';</script>");

                    Session["Basket"] = null;

                    Response.Write(builder.ToString());
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

        /*
         * @Method: registerBtn_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of register customer button.
         * A new ID will be assigned to the new customer, and password will be hashed, and
         * saved in the database using SQL Data Access Layer
         */
        protected void registerBtn_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();

            //Creating new ID for the customer
            customer.CustomerID = Guid.NewGuid();

            //Getting new hashed password
            PasswordHasher hasher = new PasswordHasher();
            string hashedPassword =
                hasher.GetHashedPassword(RegisterUserPasswordInput.Text, RegisterUserMailInput.Text);

            //Constructing the insert query
            string query = "insert into Customers values('" + customer.CustomerID + "','" +
                RegisterUserNameInput.Text + "','" + RegisterUserMailInput.Text + "','" +
                RegisterUserPhoneInput.Text + "','" + hashedPassword + "')";

            string result = dbCommander.InsertRecord(query);

            if (result == "1")
            {
                //Closing database connection
                dbCommander.CloseConnection();

                //Building success message script to redirect after success
                StringBuilder builder = new StringBuilder();
                builder.Append("<script>alert('Account created successfully');");
                builder.Append("window.location.href = '");
                builder.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                builder.Append("/default.aspx");
                builder.Append("';</script>");

                Response.Write(builder.ToString());
            }
            else
            {
                //If the user is not inserted, show that there was an error with the database
                Response.Write("<script>alert('Database error, please check your provided data to be correct');</script>");

                //Closing database connection
                dbCommander.CloseConnection();
            }
        }
    }
}