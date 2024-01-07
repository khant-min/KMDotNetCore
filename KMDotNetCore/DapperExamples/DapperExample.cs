using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KMDotNetCore.ConsoleApp.Models;

namespace KMDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public DapperExample()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder
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
            Edit(5);
            Create("test 8.21", "test 2", "test 3");
            Update(5, "test 6", "test 7", "test 8");
            Delete(5);
        }

        private void Read()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private void Edit(int id)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id
            };

            string query = "select * from tbl_blog where Blog_Id = @Blog_Id";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            //BlogDataModel? item = db.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();
            BlogDataModel? item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
        }

        private void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

               string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Successfully created." : "Creating failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            string query = @"
                            UPDATE [dbo].[Tbl_Blog]
                            SET [Blog_Title] = @Blog_Title
                                ,[Blog_Author] = @Blog_Author
                                ,[Blog_Content] = @Blog_Content
                            WHERE Blog_Id = @Blog_Id";


            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Successfully updated." : "Updating failed";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            BlogDataModel blog = new BlogDataModel() { Blog_Id = id };

            string query = @"
                            DELETE FROM [dbo].[Tbl_Blog]
                            WHERE Blog_Id = @Blog_Id";

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Successfully deleted." : "Deleting failed";
            Console.WriteLine(message);
        }
    }
}
