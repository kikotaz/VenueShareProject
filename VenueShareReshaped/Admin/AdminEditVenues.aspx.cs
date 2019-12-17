using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;
using System.IO;
using System.Text;

namespace VenueShareReshaped.Admin
{
    /*
     * @ClassName: AdminEditVenues
     * @Description: This class will be the code behind for AdminEditVenues.aspx page. It will
     * handle all the requests and back code requirements
     * @Developer: Karim Saleh
     */
    public partial class AdminEditVenues : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        //Getting an instance of the search string builder
        private SearchStringBuilder stringBuilder = SearchStringBuilder.GetInstance();

        //Global variables will be used during runtime
        static int editIndex;
        static string venueID;
        static string updateArgument;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SearchLocationList.Items.Clear();
                //This code block will bind list of cities to the drop down list of cities
                List<string> cities = new List<string> { "", "Auckland", "Wellington", "Christchurch", "Hamilton", "Taurange", "Napier-Hastings", "Dunedin", "Palmerston North", "Nelson", "Rotorua", "Whangarei", "New Plymouth", "Invercargill", "Whanganui", "Gisborne" };
                cities.Sort();
                SearchLocationList.DataSource = cities;
                SearchLocationList.DataBind();
            }
        }
        /*
         * @Method: EditVenueList_PreRender
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated before the ListView is rendered,
         * it will fetch all the approved venues from the database and bind them to 
         * the ListView object
         */
        protected void EditVenuesList_PreRender(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
            string query = stringBuilder.BuildQuery(SearchLocationList.SelectedValue,
                SearchVenueNameInput.Text, SearchCustomerInput.Text);
            SqlDataSource source = new SqlDataSource(connectionString, query);

            EditVenuesList.DataSource = source;
            EditVenuesList.DataBind();
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
         * @Method: CanecelPromoteButton_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of cancel Promote button.
         * The selected item index will be reset and the ListView items will be refreshed
         */
        protected void CancelPromoteButton_Click(object sender, EventArgs e)
        {
            EditVenuesList.SelectedIndex = -1;
            EditVenuesList.DataBind();
        }
        /*
         * @Method: EditVenueList_SelectedIndexChanging
         * @Params: object sender, ListViewSelectEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Promote or Demote button.
         * The respective item index will be assigned as selected. The ListView will be
         * refreshed after selection
         */
        protected void EditVenuesList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            EditVenuesList.SelectedIndex = e.NewSelectedIndex;
            EditVenuesList.DataBind();
        }
        /*
         * @Method: EditVenuesList_ItemEditing
         * @Params: object sender, ListViewEditEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of edit button.
         * The index of the selected item (to be edited) will be assigned for edit,
         * and the value of that index will be saved in a global variable to be used 
         * later in another method for editing
         */
        protected void EditVenuesList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            EditVenuesList.EditIndex = e.NewEditIndex;
            //This variable will be used to create edit request
            editIndex = EditVenuesList.EditIndex;
            EditVenuesList.DataBind();
        }
        /*
         * @Method: EditContentButton_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Edit item button.
         * The selected index will be updated using the global variable of the editIndex,
         * and the VenueID will sent to AdminAddVenue.aspx page to edit the information
         * of that Venue
         */
        protected void EditContentButton_Click(object sender, EventArgs e)
        {
            EditVenuesList.SelectedIndex = editIndex;
            string id = EditVenuesList.SelectedDataKey.Value.ToString();
            if (id != null)
            {
                Response.Redirect("../Admin/AdminAddVenue.aspx?id=" + id);
            }
        }
        /*
         * @Method: CancelEditButton_Click
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Cancel edit button.
         * The edit index will be reset and the ListView will be refreshed
         */
        protected void CancelEditButton_Click(object sender, EventArgs e)
        {
            EditVenuesList.EditIndex = -1;
            EditVenuesList.DataBind();
        }
        /*
         * @Method: EditVenueList_ItemDeleting
         * @Params: object sender, ListViewDeleteEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of delete venue button.
         * The selected VenueID will be assigned to a delete query, and database
         * will be updated accordingly
         */
        protected void EditVenuesList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            EditVenuesList.SelectedIndex = editIndex;
            string id = EditVenuesList.SelectedDataKey.Value.ToString();

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
                    EditVenuesList.EditIndex = -1;
                    EditVenuesList.SelectedIndex = -1;
                    EditVenuesList.DataBind();
                    
                    //Showing success message
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                        "alert('Venue deleted successfully');", true);
                }
            }
            catch (SqlException)
            {
                //If the user is not inserted, show that there was an error with the database
                Response.Write
                    ("<script>alert('Database error, please check your provided data to be correct');</script>");
            }
        }

        /*
         * @Method: EditVenueList_ItemUpdating
         * @Params: object sender, ListViewDeleteEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Promote
         * or Demote venue button. It will decide whether the command is for promotion
         * or demotion, and then update the database accordingly
         */
        protected void EditVenuesList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string query = null;
            venueID = EditVenuesList.SelectedDataKey.Value.ToString();
            if (updateArgument == "Promote")
            {
                query = "update Venues set IsPromoted=1 where VenueID ='" +
                venueID + "'";
            }
            else if (updateArgument == "Demote")
            {
                query = "update Venues set IsPromoted=0 where VenueID ='" +
                venueID + "'";
            }

            string result = dbCommander.UpdateRecord(query);

            //If update is successful
            if (result == "1")
            {
                //Closing database connection
                dbCommander.CloseConnection();

                //Turn the list into default
                EditVenuesList.SelectedIndex = -1;
                EditVenuesList.DataBind();
            }
        }
        /*
         * @Method: PromoteVenueButton_Command
         * @Params: object sender, CommandEventArgs e
         * @Return: void
         * @Description: This method will be activated on the click of Promote or
         * Demote button, whereby the Update command is activated. The method will 
         * update a global variable that will indicate whether the required operation
         * is promotion or demotion
         */
        protected void PromoteVenueButton_Command(object sender, CommandEventArgs e)
        {
            updateArgument = e.CommandArgument.ToString();
        }
    }
}