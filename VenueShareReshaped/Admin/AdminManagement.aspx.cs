using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    /*
     * @ClassName: AdminManagement
     * @Description: This class will be the code behind for AdminManagement.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class AdminManagement : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Manage Admins";

            //Assigning connection string to GridView Data source
            AdminsDataSource.ConnectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
        }

        /*
         * @Method: AddAdminBtn_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of submit admin button.
         * A new ID will be assigned to the new admin, and password will be hashed, and
         * saved in the database using SQL Data Access Layer
         */
        protected void AddAdminBtn_Click(object sender, EventArgs e)
        {
            AdminObject admin = new AdminObject();

            //Creating new unique ID to the admin object
            admin.AdminID = Guid.NewGuid();

            //Getting new hashed password
            PasswordHasher hasher = new PasswordHasher();
            string hashedPassword = 
                hasher.GetHashedPassword(AdminPasswordInput.Text, AdminEmailInput.Text);

            //Constructing the insert query
            string query = "insert into tblAdmins values('" + admin.AdminID + "','"
                + AdminNameInput.Text + "','" + AdminEmailInput.Text + "','" + hashedPassword
                + "')";

            string result = dbCommander.InsertRecord(query);

            if(result == "1")
            {
                //Success message
                Response.Write("<script>alert('Admin inserted');</script>");

                //Clearing all the input fields
                AdminNameInput.Text = string.Empty;
                AdminEmailInput.Text = string.Empty;
                AdminPasswordInput.Text = string.Empty;
            }
            else
            {
                //If the user is not inserted, show that there was an error with the database
                Response.Write
                    ("<script>alert('Database error, please check your provided data to be correct');</script>");
            }

            //Closing connection to database
            dbCommander.CloseConnection();

            //Refresh the GridView
            AdminGridView.DataBind();
        }
    }
}