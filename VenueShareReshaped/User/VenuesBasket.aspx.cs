using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.User
{
    public partial class VenuesBasket : System.Web.UI.Page
    {
        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        private static string ids;
        private static string query;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BasketList_PreRender(object sender, EventArgs e)
        {
            //Configuring Data Source for the ListView
            string connectionString = ConfigurationManager.ConnectionStrings[dbCommander.sourceString].ConnectionString;

            if(Session["Basket"] != null)
            {
                List<string> venuesBasket = (List<string>)Session["Basket"];

                if(venuesBasket.Count == 0)
                {
                    ids = "00000000-0000-0000-0000-000000000000";
                }

                else
                {
                    //Creating string from VenueID list in session
                    ids = string.Join("','", ((List<string>)Session["Basket"]).ToArray());
                }            
            }
            else
            {
                ids = "00000000-0000-0000-0000-000000000000";
            }

            //Passing the string of ids to the query
            query = String.Format("SELECT * FROM Venues INNER JOIN Customers ON Venues.CustomerID = Customers.CustomerID WHERE VenueID IN ('{0}')", ids);

            SqlDataSource source = new SqlDataSource(connectionString, query);

            BasketList.DataSource = source;
            BasketList.DataBind();
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
        
        protected void BasketList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //Removing the item from the basket
            List<string> venuesBasket = (List<string>)Session["Basket"];
            venuesBasket.RemoveAt(e.ItemIndex);
        }

        protected void BasketList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            BasketList.SelectedIndex = e.NewSelectedIndex;
            Customer customer = (Customer)Session["Customer"];
            string proposalID = Guid.NewGuid().ToString();
            

            if (ProposalDateInput.SelectedDate > DateTime.Today)
            {
                string proposalQuery = "INSERT INTO Proposals VALUES ('" + proposalID +
                "'," + ProposalDateInput.SelectedDate.ToString("dd/MM/yyyy") + 
                ",'" + customer.CustomerID + "','" + 
                BasketList.SelectedDataKey.Value.ToString() + "')";

                //Inserting new proposal to the database
                string result = dbCommander.InsertRecord(proposalQuery);

                //If insert is successful
                if (result == "1")
                {
                    //Closing database connection
                    dbCommander.CloseConnection();

                    //Removing the item from the basket
                    List<string> venuesBasket = (List<string>)Session["Basket"];
                    venuesBasket.RemoveAt(e.NewSelectedIndex);

                    //Showing success message message
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                        "alert('Proposal submitted successfully');", true);
                }
            }
            else if (ProposalDateInput.SelectedDate <= DateTime.Today)
            {
                //Showing error message message
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                    "alert('Please select proper date for your proposal');", true);
            }
        }
    }
}