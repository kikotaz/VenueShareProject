using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    /*
     * @ClassName: AdminApproveVenues
     * @Description: This class will be the code behind for AdminApproveVenue.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class AdminApproveVenues : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        //Global variables will be used during runtime
        static int editIndex;
        static string venueID;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UnApprovedList_PreRender(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
            string query = "SELECT * FROM Venues INNER JOIN	Customers ON Venues.CustomerID = Customers.CustomerID WHERE Venues.IsApproved = 0";
            SqlDataSource source = new SqlDataSource(connectionString, query);

            UnApprovedList.DataSource = source;
            UnApprovedList.DataBind();
            dbCommander.CloseConnection();
        }
        /*
         * @Method: GetImages
         * @Params: string origin
         * @Return: string, will return the first image path to the calling method
         * @Description: This method will accept a string that has all the images paths,
         * it will split the paths list, and return the first image's path
         */
        public string GetImages(string origin)
        {
            //Creating new folder for uploaded images 
            string path = Server.MapPath("/Content/Images/");

            //Removing local machine path to avoid security clash
            string final = origin.Replace(path, "/Content/Images/");

            //Splitting the images paths
            string[] images = final.Split('+');

            //Returning the first image path
            return images.First();
        }
        /*
         * @Method: CanecelApproveButton_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of cancel Approve button.
         * The selected item index will be reset and the ListView items will be refreshed
         */
        protected void CancelApproveButton_Click(object sender, EventArgs e)
        {
            UnApprovedList.SelectedIndex = -1;
            UnApprovedList.DataBind();
        }
        /*
         * @Method: CancelDeleteButton_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Cancel delete button.
         * The edit index will be reset and the ListView will be refreshed
         */
        protected void CancelDeleteButton_Click(object sender, EventArgs e)
        {
            UnApprovedList.EditIndex = -1;
            UnApprovedList.DataBind();
        }
        /*
         * @Method: UnApprovedList_SelectedIndexChanging
         * @Params: object sender, ListViewSelectEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Approve or Edit buttons.
         * The respective item index will be assigned as selected. The ListView will be
         * refreshed after selection
         */
        protected void UnApprovedList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            UnApprovedList.SelectedIndex = e.NewSelectedIndex;
            UnApprovedList.DataBind();
        }
        /*
         * @Method: UnApprovedList_ItemEditing
         * @Params: object sender, ListViewEditEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of delete button.
         * The index of the selected item (to be deleted) will be assigned for delete,
         * and the value of that index will be saved in a global variable to be used 
         * later in another method for editing
         */
        protected void UnApprovedList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            UnApprovedList.EditIndex = e.NewEditIndex;
            //This variable will be used to create edit request
            editIndex = UnApprovedList.EditIndex;
            UnApprovedList.DataBind();
        }
        /*
         * @Method: UnApprovedList_ItemUpdating
         * @Params: object sender, ListViewDeleteEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Approve
         * venue button. It will pick the VenueID and update the database
         * accordingly
         */
        protected void UnApprovedList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string query = null;
            venueID = UnApprovedList.SelectedDataKey.Value.ToString();

            query = "update Venues set IsApproved=1 where VenueID ='" +
                venueID + "'";

            string result = dbCommander.UpdateRecord(query);

            //If update is successful
            if (result == "1")
            {
                //Closing database connection
                dbCommander.CloseConnection();

                //Turn the list into default
                UnApprovedList.SelectedIndex = -1;
                UnApprovedList.DataBind();

                //Showing success message
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('Venue approved successfully');", true);
            }
        }
        /*
         * @Method: UnApprovedList_ItemDeleting
         * @Params: object sender, ListViewDeleteEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of delete venue button.
         * This method will be activated on the click of delete venue button.
         * The selected VenueID will be assigned to a delete query, and database
         * will be updated accordingly
         */
        protected void UnApprovedList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            UnApprovedList.SelectedIndex = editIndex;
            string id = UnApprovedList.SelectedDataKey.Value.ToString();

            try
            {
                //Delete the images folder
                string path = Server.MapPath("/Content/Images/") + id;
                DirectoryInfo directory = Directory.CreateDirectory(path);
                directory.Delete(true);

                string query = "delete from Venues where VenueID='" + id + "'";

                string result = dbCommander.UpdateRecord(query);

                //If update is successful
                if (result == "1")
                {
                    //Closing database connection
                    dbCommander.CloseConnection();

                    //Turn the list into default
                    UnApprovedList.EditIndex = -1;
                    UnApprovedList.SelectedIndex = -1;
                    UnApprovedList.DataBind();

                    //Showing success message
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                        "alert('Venue deleted successfully');", true);
                }
            }
            catch (SqlException)
            {
                //If the user is not inserted, show that there was an error with the database
                //Showing success message
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('Database error, please check your provided data to be correct');", true);
            }
        }
    }
}