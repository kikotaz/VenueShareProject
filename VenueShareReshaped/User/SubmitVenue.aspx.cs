using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VenueShareReshaped.Models;

namespace VenueShareReshaped.User
{
    /*
     * @ClassName: SubmitVenue
     * @Description: This class will be the code behind for SubmitVenue.aspx page. It will
     * handle all the requests and back code requirements to allow customer to submit new
     * venue to the database
     * @Developer: Karim Saleh
     */
    public partial class SubmitVenue : System.Web.UI.Page
    {
        //Assigning some global variables
        static Customer customer;
        static Venue venue;

        //Getting an instance of DataLayer (Singleton)
        private DataLayer dbCommander = DataLayer.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            customer = (Customer)Session["Customer"];
            if (!IsPostBack)
            {
                venueCityInput.Items.Clear();
                //This code block will bind list of cities to the drop down list of cities
                List<string> cities = new List<string> { "", "Auckland", "Wellington", "Christchurch", "Hamilton", "Taurange", "Napier-Hastings", "Dunedin", "Palmerston North", "Nelson", "Rotorua", "Whangarei", "New Plymouth", "Invercargill", "Whanganui", "Gisborne" };
                cities.Sort();
                venueCityInput.DataSource = cities;
                venueCityInput.DataBind();
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

                string query = "insert into Venues values ('" + venue.VenueID + "','" +
                    venueNameInput.Text + "','" + venueAddressInput.Text + "','" +
                    venueCityInput.Text + "', " + Int16.Parse(venueAreaInput.Text) +
                    ", " + Int16.Parse(venueCapacityInput.Text) + ",'" +
                    venueDescriptionInput.Text + "','" + venue.Images +
                    "','" + customer.CustomerID.ToString() + "',0,0)";

                //Inserting new Venue to the database
                string result = dbCommander.InsertRecord(query);

                //If insert is successful
                if (result == "1")
                {
                    //Closing database connection
                    dbCommander.CloseConnection();

                    //Building success message script to redirect after success
                    StringBuilder builder = new StringBuilder();
                    builder.Append("<script>alert('Venue added successfully');");
                    builder.Append("window.location.href = '");
                    builder.Append(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                    builder.Append("/default.aspx");
                    builder.Append("';</script>");

                    Response.Write(builder.ToString());
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
    }
}