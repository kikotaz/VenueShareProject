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
    public partial class AdminProposals : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ProposalsList_PreRender(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;

            string query = "SELECT * FROM Proposals JOIN Venues ON Proposals.VenueID " +
                "= Venues.VenueID JOIN Customers ON Proposals.CustomerID = Customers.CustomerID";

            SqlDataSource source = new SqlDataSource(connectionString, query);

            ProposalsList.DataSource = source;
            ProposalsList.DataBind();
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
    }
}