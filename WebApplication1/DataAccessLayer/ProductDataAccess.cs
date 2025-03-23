using System.Data;
using WebApplication1.Models;
using MySql.Data.MySqlClient;

namespace WebApplication1.DataAccessLayer;

public class ProductDataAccess
{
    private string connString;

    public ProductDataAccess()
    {
        connString = "Data Source=127.0.0.1,3306; Database=Inventory; User ID=superuser; Password=1234;";
    }

    public bool CreateProduct(string name, int quantity, int customerId)
    {
        bool created = false;
        using (MySqlConnection sqlConnection = new MySqlConnection(connString))
        {
            sqlConnection.Open();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("create_product", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.Parameters.AddWithValue("customer_id", customerId);
                    cmd.ExecuteNonQuery();
                    created = true;
                }
            }
            catch
            {
                created = false;
            }
        }

        return created;
    }

    public ProductModel GetProduct(int id)
    {
        ProductModel product = null;

        using (MySqlConnection sqlConnection = new MySqlConnection(connString))
        {
            sqlConnection.Open();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("read_product", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        product = new ProductModel
                        (
                            Convert.ToInt32(reader["id"]),
                            reader["name"].ToString(),
                            Convert.ToInt32(reader["quantity"]),
                            Convert.ToInt32(reader["customer_id"])
                        );
                    }
                    
                }
            }
            catch
            {
                product = null;
            }
        }


        return product;
    }
    
    public List<ProductModel> GetProductList()
    {
        List<ProductModel> productList = new List<ProductModel>();
        using (MySqlConnection sqlConnection = new MySqlConnection(connString))
        {
            sqlConnection.Open();
            using (MySqlCommand cmd = new MySqlCommand("read_all_products", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel
                        (
                            Convert.ToInt32(reader["id"]),
                            reader["name"].ToString(),
                            Convert.ToInt32(reader["quantity"]),
                            Convert.ToInt32(reader["customer_id"])
                        );
                        productList.Add(product);
                    }
                }
            }
        }

        return productList;
    }

    public bool UpdateProduct(int id, string name, int quantity, int customerId)
    {
        bool updated = false;
        using (MySqlConnection sqlConnection = new MySqlConnection(connString))
        {
            sqlConnection.Open();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("update_product", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.Parameters.AddWithValue("customer_id", customerId);
                    cmd.ExecuteNonQuery();
                    updated = true;
                }
            }
            catch
            {
                updated = false;
            }
        }

        return updated;
    }

    public bool DeleteProduct(int id)
    {
        bool deleted = false;
        using (MySqlConnection sqlConnection = new MySqlConnection(connString))
        {
            sqlConnection.Open();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("delete_product", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    deleted = true;
                }
            }
            catch
            {
                deleted = false;
            }
        }
        
        return deleted;
    }
}