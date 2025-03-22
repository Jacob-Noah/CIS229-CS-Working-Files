using System.Data;
using MySql.Data.MySqlClient;
using WebApplication1.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace WebApplication1.DataAccessLayer
{
    public class UserDataAccess
    {
        private string connString;

        public UserDataAccess()
        {
            // connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
            // connString = ConfigurationManager.AppSettings["DatabaseConnString"];
            // Console.WriteLine($"THE CONNECTION STRING : {connString}");

            connString = "Data Source=127.0.0.1,3306; Database=Inventory; User ID=superuser; Password=1234;";
        }

        public bool GetUser(string usernameI, string passwordI)
        {
            bool userExists = false;
            using (MySqlConnection sqlConnection = new MySqlConnection(connString))
            {
                sqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand("does_account_exist", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("username", usernameI);
                    cmd.Parameters.AddWithValue("password", passwordI);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        userExists = true;
                    }
                }
            }

            return userExists;
        }

        public List<UserModel> GetUserList()
        {
            List<UserModel> userList = new List<UserModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connString))
            {
                sqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand("read_all_accounts", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            (
                                reader["username"].ToString(),
                                reader["password"].ToString()
                            );
                            userList.Add(user);
                        }
                    }
                }
            }

            return userList;
        }
    }
}