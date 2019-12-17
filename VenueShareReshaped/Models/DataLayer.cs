using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VenueShareReshaped.Models
{
    /*
     * @ClassName: DataLayer
     * @Description: This class will serve as Data Access Layer between
     * the application and the database
     * @Developer: Karim Saleh
     */
    public class DataLayer
    {
        private static DataLayer instance = null;
        private SqlConnection connection;
        public string sourceString = "VenueShare";
        private SqlDataReader reader;

        //Setting up the constructor for DataLayer class to open new SQL Connection
        public DataLayer()
        {
            //Assigning connection string
            string configuration = ConfigurationManager.ConnectionStrings[sourceString].ConnectionString;

            connection = new SqlConnection(configuration);
        }

        /*
         * @Method: GetInstance
         * @Params: nil
         * @Return: DataLayer (Object), will return an instance of this class 
         * @Description: This method will be used to get an instance of the this
         * DataLayer class. It will make sure that only one instance is used in the
         * application to avoid memory issues. This method is following Singleton 
         * design pattern
         */
        public static DataLayer GetInstance()
        {
            //Will create new instance if the DataLayer is empty
            if(instance == null)
            {
                instance = new DataLayer();
            }
            return instance;
        }

        /*
         * @Method: InsertRecord
         * @Params: string query (Query string to be executed)
         * @Return: string, will return "1" if the record is successfully inserted,
         * or will retrung the Exception message if there is a SqlException
         * @Description: This method will be used to insert new record to the database
         * regardless to the required table. The table will be defined in the query,
         * which will be executed by SQL Command object
         */
        public string InsertRecord(string query)
        {
            string insertionResult;
            try
            {
                //Open SQL connection if closed
                if(connection.State == ConnectionState.Closed)
                {
                    OpenConnection();
                }
                SqlCommand command = new SqlCommand(query, connection);
                insertionResult = command.ExecuteNonQuery().ToString();
            }
            catch(SqlException ex)
            {
                insertionResult = ex.Message.ToString();
            }

            return insertionResult;
        }

        /*
         * @Method: UpdateRecord
         * @Params: string query (Query string to be executed)
         * @Return: string, will return "1" if the record is successfully updated,
         * or will retrung the Exception message if there is a SqlException
         * @Description: This method will be used to update existing record in the database
         * regardless to the required table. The table will be defined in the query,
         * which will be executed by SQL Command object
         */
        public string UpdateRecord(string query)
        {
            string updateResult;
            try
            {
                //Open SQL connection if closed
                if (connection.State == ConnectionState.Closed)
                {
                    OpenConnection();
                }
                SqlCommand command = new SqlCommand(query, connection);
                updateResult = command.ExecuteNonQuery().ToString();
            }
            catch (SqlException ex)
            {
                updateResult = ex.Message.ToString();
            }

            return updateResult;
        }

        /*
         * @Method: ReadRecord
         * @Params: string query (Query string to be executed)
         * @Return: SqlDataReader object, will return the data reader object,
         * which has the data retrieved from the database
         * @Description: This method will be used to read record from the database
         * regardless to the required table. The table will be defined in the query,
         * which will be executed by SQL Command object
         */
        public SqlDataReader ReadRecord(string query)
        {
            try
            {
                //Open SQL connection if closed
                if (connection.State == ConnectionState.Closed)
                {
                    OpenConnection();
                }
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
            }
            catch(SqlException)
            {
                return null;
            }

            return reader;
        }

        /*
         * @Method: OpenConnection
         * @Params: Nil
         * @Return: void
         * @Description: This method will only open SQL Connection
         */
        public void OpenConnection()
        {
            connection.Open();
        }
        /*
         * @Method: CloseConnection
         * @Params: Nil
         * @Return: void
         * @Description: This method will only close the opened SQL Connection
         */
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}