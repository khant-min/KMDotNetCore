using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMDotNetCore.MvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KMDotNetCore.MvcApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = ".",
                    InitialCatalog = "ALTDotNetCore",
                    UserID = "sa",
                    Password = "sa@123",
                    TrustServerCertificate = true,
                };
                optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            }
        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
