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

        public void Run(string runCmd, string title = null, string author = null, string content = null)
        {
            if (runCmd == "read")
            {
                Read();
            } else
            {
                Create(title, author, content);
            }
        }
       
        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "ALTDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
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

        private void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "ALTDotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
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


    }
}
