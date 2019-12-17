using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    /*
    * @ClassName: AdminAddCustomer
    * @Description: This class will be the code behind for AdminAddCustomer.aspx page. It will
    * handle all the requests and back code requirements
    * @Developer: Karim Saleh
    */
    public partial class AdminAddCustomer : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /*
         * @Method: registerBtn_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated when Add Customer button is
         * clicked. The method will create a new customer object, hash the password,
         * and add the customer to the database accordingly
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
                builder.Append("alert('Account created successfully');");
                builder.Append("window.location.href = '");
                builder.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                builder.Append("/Admin/AdminManageCustomer.aspx");
                builder.Append("';");

                builder.ToString();
            }
            else
            {
                //If the user is not inserted, show that there was an error with the database
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('Database error, please check your provided data to be correct');", true);
               
                //Closing database connection
                dbCommander.CloseConnection();
            }
        }
    }
}