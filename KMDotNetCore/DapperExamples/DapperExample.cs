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
    }
}
