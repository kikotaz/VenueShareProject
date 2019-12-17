using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.User
{
    /*
     * @ClassName: VenueDetails
     * @Description: This class will be the code behind for VenueDetails.aspx page. It will
     * handle all the requests and back code requirements to allow customer view a specific 
     * Venue details and issue proposal 
     * @Developer: Karim Saleh
     */
    public partial class VenueDetails : System.Web.UI.Page
    {
        //Assigning some global variables
        static Customer customer;
        static string[] images;
        static SqlDataSource source;
        static List<string> venuesBasket;

        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            customer = (Customer)Session["Customer"]; 
        }
        /*
         * @Method: GetImages
         * @Params: string origin
         * @Return: string array, will return an array of strings that has all the images paths
         * @Description: This method will accept a string that has all the images paths,
         * it will split the paths list, and add them to a string array, then return the array
         */
        public string[] GetImages(string origin)
        {
            //Creating new folder for uploaded images 
            string path = Server.MapPath("/Content/Images/");

            //Removing local machine path to avoid security clash
            string final = origin.Replace(path, "/Content/Images/");

            //Splitting the images paths
            string[] images = final.Split('+');

            images = images.Where(i => i != "").ToArray();

            //Returning the images paths
            return images;
        }
        /*
         * @Method: VenueDetailsView_PreRender
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated before the Venue details
         * ListView is rendered to the page
         */
        protected void VenueDetailsView_PreRender(object sender, EventArgs e)
        {
            VenueDetailsView.DataSource = source;
            VenueDetailsView.DataBind();
            dbCommander.CloseConnection();
        }
        /*
         * @Method: ImageRepeater_Load
         * @Params: object sender, EventArgs e
         * @Return: void
         * @Description: This method will be activated at the before the images
         * repeater is loaded to assign the images string as Data Source, and assign
         * the SqlDataSource at the same time
         */
        protected void ImageRepeater_Load(object sender, EventArgs e)
        {
            //Configuring Data Source for all the views
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
            string query = "SELECT * FROM Venues INNER JOIN	Customers ON Venues.CustomerID = Customers.CustomerID WHERE Venues.VenueID = '" + Request.QueryString["id"] + "'";
            source = new SqlDataSource(connectionString, query);

            //Getting the array that carries the paths to all images
            var dataview = (DataView)source.Select(DataSourceSelectArguments.Empty);
            foreach(DataRowView drv in dataview)
            {
                images = GetImages(drv["Images"].ToString());
            }            

            //Assigning the array as data source for repeater
            ImageRepeater.DataSource = images;
            ImageRepeater.DataBind();
            dbCommander.CloseConnection();
        }

        protected void VenueDetailsView_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            //Check if the user is logged in or not
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Showing login required message
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('You need to login to add to basket');", true);
            }
            else
            {
                //Creating new basket if doesn't exist
                if(Session["Basket"] == null)
                {
                    venuesBasket = new List<string>();
                }
                else
                {
                    venuesBasket = (List<string>)Session["Basket"];
                }

                //Adding new venue to the basket
                VenueDetailsView.SelectedIndex = e.NewSelectedIndex;
                venuesBasket.Add(VenueDetailsView.SelectedDataKey.Value.ToString());
                //Assigning the updated list to the basket session
                Session["Basket"] = venuesBasket;

                //Showing success message message
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('This venue is added successfully to your venues basket');", true);
            }
        }
    }
}