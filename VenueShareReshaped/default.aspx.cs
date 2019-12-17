using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped
{
    public partial class _default : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();
        
        static string venueID;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HomeVenues_Load(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
            string query = "SELECT * FROM Venues INNER JOIN	Customers ON Venues.CustomerID = Customers.CustomerID WHERE Venues.IsPromoted = 1";
            SqlDataSource source = new SqlDataSource(connectionString, query);

            HomeVenues.DataSource = source;
            HomeVenues.DataBind();
            dbCommander.CloseConnection();
        }

        //This method will get the paths for the images
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

        protected void HomeVenues_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            HomeVenues.SelectedIndex = e.NewSelectedIndex;

            string id = HomeVenues.SelectedDataKey.Value.ToString();

            Response.Redirect("/User/VenueDetails.aspx?id=" + id);
        }
    }
}