using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace WebApplication1.DataAccessLayer
{
    public class UserDataAccess
    {
        public Boolean GetUser(string usernameI, string passwordI)
        {
            String connString = ConfigurationManager.AppSettings["DatabaseConnString"];
            SqlConnection sqlConnection = new SqlConnection(connString);
            try
            {
                sqlConnection.Open();
                String query = "SELECT * FROM ACCOUNT  WHERE userName='"+usernameI+"' AND password ='"+passwordI+"'";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                 
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Reads the first row of the result set
                    {

                        return true;

                    }

                }

                sqlConnection.Close();
            }

            catch (Exception e)
            {
                sqlConnection.Close();
            }

            return false;

        }
    }
}