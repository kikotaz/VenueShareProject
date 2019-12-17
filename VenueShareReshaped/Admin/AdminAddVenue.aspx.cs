using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.Admin
{
    /*
    * @ClassName: AdminAddVenue
    * @Description: This class will be the code behind for AdminAddVenues.aspx page. It will
    * handle all the requests and back code requirements
    * @Developer: Karim Saleh
    */
    public partial class AdminAddVenue : System.Web.UI.Page
    {
        //Assigning some global variables
        static Customer customer;
        static Venue venue;
        static string urlBefore;

        //Getting an instance of DataLayer (Singleton)
        private static DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Add New Venue";

            if (!IsPostBack)
            {
                venueCityInput.Items.Clear();
                //This code block will bind list of cities to the drop down list of cities
                List<string> cities = new List<string> { "", "Auckland", "Wellington", "Christchurch", "Hamilton", "Taurange", "Napier-Hastings", "Dunedin", "Palmerston North", "Nelson", "Rotorua", "Whangarei", "New Plymouth", "Invercargill", "Whanganui", "Gisborne" };
                cities.Sort();
                venueCityInput.DataSource = cities;
                venueCityInput.DataBind();

                //This block of code will check if the page is loaded from admin
                //By checking if it has venue ID to populate its data
                //This will help to edit the venue data
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Guid id = Guid.Parse(Request.QueryString["id"]);

                    //Building read query
                    string query = "select * from Venues where VenueID='" + id +
                        "'";
                    venue = new Venue();
                    customer = new Customer();

                    //Getting reader result 
                    var reader = dbCommander.ReadRecord(query);

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            venueNameInput.Text = (string)reader["Name"];
                            venueCityInput.SelectedValue = (string)reader["City"];
                            venueAddressInput.Text = (string)reader["Address"];
                            venueAreaInput.Text = reader["Area"].ToString();
                            venueCapacityInput.Text = reader["Capacity"].ToString();
                            venueDescriptionInput.Text = (string)reader["Description"];
                            venue.VenueID = (Guid)reader["VenueID"];
                            customer.CustomerID = (Guid)reader["CustomerID"];
                        }
                    }

                    dbCommander.CloseConnection();
                    venueImagesValidaotr.Enabled = false;
                }
                //Getting the url that the request is redirected from
                urlBefore = Request.UrlReferrer.ToString();

            }

            //This code block to properly write the Area label
            venueAreaLabel.Text = "Area (m\u00b2)";
        }

        protected void submitVenueBtn_Click(object sender, EventArgs e)
        {
            if (venue == null)
            {
                venue = new Venue();
                venue.VenueID = Guid.NewGuid();
                venue.CustomerID = customer.CustomerID;
            }

            try
            {
                //Creating new folder for uploaded images 
                string path = Server.MapPath("/Content/Images/") + venue.VenueID.ToString();
                DirectoryInfo newDirectory = Directory.CreateDirectory(path);

                StringBuilder finalPaths = new StringBuilder();

                //Checking if the uploaded images are less than 5 and not from Admin
                if (venueImagesUpload.PostedFiles.Count <= 5 &&
                    string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    foreach (HttpPostedFile img in venueImagesUpload.PostedFiles)
                    {
                        var resizedImage = ResizeImages(img);

                        string newPath = path + "/" + img.FileName;

                        using (var imgStream = new FileStream(newPath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            resizedImage.Seek(0, SeekOrigin.Begin);
                            resizedImage.CopyTo(imgStream);
                        }

                        finalPaths.Append(newPath);
                        finalPaths.Append("+");

                        //Converting paths into Byte array to save in database
                        venue.Images = finalPaths.ToString();
                    }
                }
                //Cheching if the images count is more than 5
                else if (venueImagesUpload.PostedFiles.Count > 5)
                {
                    venueImagesUploaded.Text = "Please upload maximum of 5 images";
                    newDirectory.Delete();
                }
                //If the request has VenueID
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    string query = "update Venues set Name ='" + venueNameInput.Text + "', Address ='"
                        + venueAddressInput.Text + "', City ='" + venueCityInput.Text + "', Area = " +
                        venueAreaInput.Text + ", Capacity = " +
                        venueCapacityInput.Text + ", Description ='" +
                        venueDescriptionInput.Text + "' where VenueID = '" +
                        Request.QueryString["id"].ToString() + "'";

                    string result = dbCommander.UpdateRecord(query);

                    //If update is successful
                    if (result == "1")
                    {
                        //Closing database connection
                        dbCommander.CloseConnection();

                        //Building success message script to redirect after success
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<script>alert('Venue Updated successfully');");
                        builder.Append("window.location.href = '");
                        builder.Append(urlBefore);
                        builder.Append("';</script>");

                        Response.Write(builder.ToString());
                    }
                }
                else
                {
                    string query = "insert into Venues values ('" + venue.VenueID + "','" +
                        venueNameInput.Text + "','" + venueAddressInput.Text + "','" +
                        venueCityInput.Text + "', " + venueAreaInput.Text +
                        ", " + venueCapacityInput.Text + ",'" +
                        venueDescriptionInput.Text + "','" + venue.Images +
                        "','" + customer.CustomerID.ToString() + "',1,0)";

                    string result = dbCommander.InsertRecord(query);

                    //If insert is successful
                    if (result == "1")
                    {
                        //Closing database connection
                        dbCommander.CloseConnection();

                        //Showing success message message
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert",
                            "alert('Venue added successfully');", true);

                        Response.Redirect("/Admin/AdminEditVenues.aspx");

                    }
                }
            }
            catch (SqlException)
            {
                //If there is any issue with the database, show error
                Response.Write
                    ("<script>alert('An error occured while performing database operation, please check the given information and resubmit');</script>");

                //Closing connection after exception
                dbCommander.CloseConnection();
            }
        }

        //This method will resize images and return them
        public static MemoryStream ResizeImages(HttpPostedFile img)
        {
            //Initializing new resizer
            ImageFactory resizer = new ImageFactory(preserveExifData: true);

            //Creating memory stream that will be returned
            MemoryStream output = new MemoryStream();

            //Adjusting output format
            ISupportedImageFormat format = new JpegFormat();

            //Adjusting output size
            Size size = new Size(400, 400);

            //Applying resizing and saving to output memory stream
            resizer.Load(img.InputStream).Resize(size).Format(format).Save(output);

            //Returning resized stream
            return output;
        }

        //This method will get the user details
        [WebMethod]
        public static Customer GetCustomer(string email)
        {
            customer = new Customer();

            string query = "select * from Customers where CustomerMail ='" + email + "'";

            var reader = dbCommander.ReadRecord(query);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer.CustomerID = (Guid)reader["CustomerID"];
                }
            }
            else
            {
                dbCommander.CloseConnection();
                return null;
            }

            dbCommander.CloseConnection();
            return customer;
        }
    }
}