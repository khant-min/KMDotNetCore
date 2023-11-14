using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KMDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext;

        public EFCoreExample()
        {
            _dbContext = new AppDbContext();
        }

        public void Run()
        {
            Read();
            Edit(8);
            Update(8, "my title", "my author", "my content");
            Create("deadpool-3", "james_gunn", "about deadpool and woverine");
            Delete(8);
        }

        private void Read()
        {
            List<BlogDataModel> lst = _dbContext.Blogs.ToList();

            foreach (BlogDataModel blog in lst)
            {
                Console.WriteLine(blog.Blog_Id);
                Console.WriteLine(blog.Blog_Title);
                Console.WriteLine(blog.Blog_Author);
                Console.WriteLine(blog.Blog_Content);
            }
        }

        private void Edit(int id)
        {
            BlogDataModel? blog = _dbContext.Blogs.SingleOrDefault(item => item.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(blog.Blog_Id);
            Console.WriteLine(blog.Blog_Title);
            Console.WriteLine(blog.Blog_Author);
            Console.WriteLine(blog.Blog_Content);
        }

        private void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Created successfully." : "Creating failed!";
            Console.WriteLine(message);
            Console.WriteLine(blog.Blog_Id);
        }

        private void Update(int id, string title, string author, string content)
        {
            BlogDataModel? blog = _dbContext.Blogs.SingleOrDefault(item => item.Blog_Id == id);
            
            if (blog is null)
            {
                Console.WriteLine("No data found!");
                return;
            }

            blog.Blog_Title = title;
            blog.Blog_Author = author;
            blog.Blog_Content = content;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updated successfully." : "Updating failed!";
            Console.WriteLine(message);
            Console.WriteLine(blog.Blog_Id);

        }
        private void Delete(int id)
        {
            BlogDataModel? blog = _dbContext.Blogs.FirstOrDefault(item => item.Blog_Id == id);
            if (blog is null)
            {
                Console.WriteLine("No data found!");
                return;
            }

            _dbContext.Blogs.Remove(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleted successfully." : "Deleting failed!";
            Console.WriteLine(message);
            Console.WriteLine(blog.Blog_Id);
        }
    }

}
