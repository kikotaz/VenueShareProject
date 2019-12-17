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
    public partial class ApprovedVenues : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        //Getting an instance of the search string builder
        private SearchStringBuilder stringBuilder = SearchStringBuilder.GetInstance();

        static string nextDirection;
        static string SortExpression;
        static string venueID;

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
        protected void ApprovedList_PreRender(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;
            string query = stringBuilder.BuildCustomerQuery(SearchLocationList.SelectedValue,
                SearchVenueInfoInput.Text);
            SqlDataSource source = new SqlDataSource(connectionString, query);
            //Converting the SqlDataSource into DataView to sort easily
            var dataview = (DataView)source.Select(DataSourceSelectArguments.Empty);
            //Assigning the sort expresssion and direction
            if(SortExpression != null && nextDirection != null)
            {
                dataview.Sort = SortExpression + " " + nextDirection;
            }

            ApprovedList.DataSource = dataview;
            ApprovedList.DataBind();
            dbCommander.CloseConnection();
        }
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

        protected void ApprovedList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ApprovedList.SelectedIndex = e.NewSelectedIndex;

            string id = ApprovedList.SelectedDataKey.Value.ToString();

            Response.Redirect("/User/VenueDetails.aspx?id=" + id);
        }

        protected void CapacitySort_Click(object sender, EventArgs e)
        {
            ChangeDirection();
            SortExpression = "Capacity";
        }

        protected void AreaSort_Click(object sender, EventArgs e)
        {
            ChangeDirection();
            SortExpression = "Area";
        }

        protected void AlphabaticalSort_Click(object sender, EventArgs e)
        {
            ChangeDirection();
            SortExpression = "Name";
        }
        public void ChangeDirection()
        {
            if (nextDirection == null)
            {
                nextDirection = "DESC";
            }
            else if (nextDirection == "DESC")
            {
                nextDirection = "ASC";
            }
            else if (nextDirection == "ASC")
            {
                nextDirection = "DESC";
            }
        }
    }
}