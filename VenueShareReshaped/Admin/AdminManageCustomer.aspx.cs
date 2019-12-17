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
     * @ClassName: AdminManageCustomer
     * @Description: This class will be the code behind for AdminManageCustomer.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class AdminManageCustomer : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();
        //The query that will be used for search
        private static string selectQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Assigning connection string to GridView Data source
            CustomersDataSource.ConnectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
        }
        /*
         * @Method: CustomerGrid_PreRender
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated before the GridView of customers
         * is rendered. It will decide if there is search query required or not, and 
         * assign the proper query to the select command to update GridView items
         * accordingly
         */
        protected void CustomerGrid_PreRender(object sender, EventArgs e)
        {
            if (SearchCustomerInput.Text == "")
            {
                selectQuery = "SELECT * FROM Customers";
            }
            else if (SearchCustomerInput.Text != "")
            {
                selectQuery = "SELECT * FROM Customers WHERE Customers.CustomerName LIKE '%" +
                    SearchCustomerInput.Text + "%' OR Customers.CustomerMail LIKE '%" +
                    SearchCustomerInput.Text + "%'";
            }

            CustomersDataSource.SelectCommand = selectQuery;
        }
        //This method will show confirmation when customer is deleted
        protected void CustomerGrid_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Showing success message
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                "alert('Customer deleted successfully');", true);
        }
        //This method will show confirmation when customer is updated
        protected void CustomerGrid_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Showing success message
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                "alert('Customer updated successfully');", true);
        }
    }
}