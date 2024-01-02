using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public AdoDotNetExample()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "ALTDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
        }

        public void Run()
        {
            Read();
            Create("test1", "test 2", "test 3");
            Edit(1);
            Update(1, "test 6", "test 7", "test 8");
            Delete(1);
        }
       
        private void Read()
        { 
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection opening...");
            connection.Open();
            Console.WriteLine("Connection opended.");

            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            Console.WriteLine("cmd: ", cmd);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            Console.WriteLine("adapter: ", adapter);

            DataTable dt = new DataTable();
            Console.WriteLine("dt: ", dt);

            adapter.Fill(dt);

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection closed.");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Blog_Id"]);
                Console.WriteLine(row["Blog_Title"]);
                Console.WriteLine(row["Blog_Author"]);
                Console.WriteLine(row["Blog_Content"]);
            }
        }

        private void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where Blog_Id = @Blog_Id;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }
            DataRow row = dt.Rows[0];

            Console.WriteLine(row["Blog_Id"]);
            Console.WriteLine(row["Blog_Title"]);
            Console.WriteLine(row["Blog_Author"]);
            Console.WriteLine(row["Blog_Content"]);
        }

        private void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                               ([Blog_Title]
                               ,[Blog_Author]
                               ,[Blog_Content])
                          VALUES
                               (@Blog_Title
                               ,@Blog_Author
                               ,@Blog_Content)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine("result: ", result);

            connection.Close();
            string message = result > 0 ? "Your data is saved successfully." : "Error occurs when saving!";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"
                            UPDATE [dbo].[Tbl_Blog]
                            SET [Blog_Title] = @Blog_Title
                                ,[Blog_Author] = @Blog_Author
                                ,[Blog_Content] = @Blog_Content
                            WHERE Blog_Id = @Blog_Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Updating successful." : "Updating failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query =  @"
                            DELETE FROM [dbo].[Tbl_Blog]
                            WHERE Blog_Id = @Blog_Id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Delete successful." : "Delete failed!";
            Console.WriteLine(message);

        }

    }
}
