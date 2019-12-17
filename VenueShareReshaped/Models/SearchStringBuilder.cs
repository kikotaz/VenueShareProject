using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenueShareReshaped.Models
{
    /*
     * @ClassName: SearchStringBuilder
     * @Description: This class will be responsible for managing the process of building
     * search queries that will be used by ListView objects. The queries will depend on the
     * search criteria provided by the front end objects
     * @Developer: Karim Saleh
     */
    public class SearchStringBuilder
    {
        /*Creating empty instance of this class to apply singleton
        *design pattern to make sure that there is only one instance
        * of the object in the memory
        */
        private static SearchStringBuilder instance = null;
        static string query;
        /*
         * @Method: GetInstance
         * @Params: nil
         * @Return: SearchStringBuilder
         * @Description: This method will check if there is an instance
         * of the object in the memory. If it already exists, it will be
         * returned to the calling class. If it is not instantiated yet,
         * a new instance will be created and returned
         */
        public static SearchStringBuilder GetInstance()
        {
            //Will create new instance if the DataLayer is empty
            if (instance == null)
            {
                instance = new SearchStringBuilder();
            }
            return instance;
        }
        /*
         * @Method: BuildQuery
         * @Params: string city, string venueName, string customerInfo
         * @Return: string, the final query 
         * @Description: This method will build a full search query depending
         * on the given search criteria by the front end objects. Maximum of two
         * criteria will be used each time. 
         */
        public string BuildQuery(string city, string venueName, string customerInfo)
        {
            string sharedBody = "SELECT * FROM Venues INNER JOIN Customers ON Venues.CustomerID = Customers.CustomerID WHERE Venues.IsApproved = 1";

            //No search criteria is used
            if (city == "" && venueName == "" && customerInfo == "")
            {
                query = sharedBody;
            }
            //Search only by city name
            else if (city != "" && venueName == "" && customerInfo == "")
            {
                query = sharedBody + " AND Venues.City ='" +
                    city + "'";
            }
            //Search only by Venue name
            else if (city == "" && venueName != "" && customerInfo == "")
            {
                query = sharedBody + " And Venues.Name LIKE '%" +
                    venueName + "%'";
            }
            //Search only by customer information
            else if (city == "" && venueName == "" && customerInfo != "")
            {
                query = sharedBody + " And (Customers.CustomerName LIKE '%" +
                    customerInfo + "%' OR Customers.CustomerMail LIKE '%" +
                    customerInfo + "%')";
            }
            //Search by city name and Venue name
            else if(city != ""  && venueName != "" && customerInfo == "")
            {
                query = sharedBody + " AND (Venues.City ='" +
                    city + "'" + " And Venues.Name LIKE '%" +
                    venueName + "%')";
            }
            //Search by city name and customer information
            else if(city != "" && venueName == "" && customerInfo != "")
            {
                query = sharedBody + " AND (Venues.City ='" +
                    city + "'" + " And (Customers.CustomerName LIKE '%" +
                    customerInfo + "%' OR Customers.CustomerMail LIKE '%" +
                    customerInfo + "%'))";
            }
            //Search by Venue name and customer information
            else if(city == "" && venueName != "" && customerInfo != "")
            {
                query = sharedBody + " And (Venues.Name LIKE '%" +
                    venueName + "%'" + " And (Customers.CustomerName LIKE '%" +
                    customerInfo + "%' OR Customers.CustomerMail LIKE '%" +
                    customerInfo + "%'))";
            }

            return query;
        }
        /*
         * @Method: BuildCustomerQuery
         * @Params: string city, string venueInfo
         * @Return: string, the final query 
         * @Description: This method will build a full search query depending
         * on the given search criteria by the front end objects. Maximum of two
         * criteria will be used each time. 
         */
        public string BuildCustomerQuery (string city, string venueInfo)
        {
            string sharedBody = "SELECT * FROM Venues INNER JOIN	Customers ON Venues.CustomerID = Customers.CustomerID WHERE Venues.IsApproved = 1";

            //No search criteria is used
            if (city == "" && venueInfo == "")
            {
                query = sharedBody;
            }
            //Search only by city name
            else if (city != "" && venueInfo == "")
            {
                query = sharedBody + " AND Venues.City ='" +
                    city + "'";
            }
            //Search only by venue information
            else if (city == "" && venueInfo != "")
            {
                query = sharedBody + " And (Venues.Name LIKE '%" +
                    venueInfo + "%' OR Venues.Description LIKE '%" +
                    venueInfo + "%')";
            }
            //Search by city name and venue information
            else if (city != "" && venueInfo != "")
            {
                query = sharedBody + " AND (Venues.City ='" +
                    city + "'" + " And (Venues.Name LIKE '%" +
                    venueInfo + "%' OR Venues.Description LIKE '%" +
                    venueInfo + "%'))";
            }

            return query;
        }
    }
}