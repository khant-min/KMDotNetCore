using KMDotNetCore.RestApi.EFCoreExamples;
using KMDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                AppDbContext db = new AppDbContext();
                List<BlogDataModel> blogs = db.Blogs.ToList();
                // BlogListResponseModel model = new BlogListResponseModel();
                // model.IsSuccess = true;
                // model.Message = "Success";
                // model.Data = blogs;

                BlogListResponseModel model = new BlogListResponseModel
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = blogs
                };

                return Ok(model);
            } catch (Exception ex)
            {
                return Ok(new BlogListResponseModel
                {
                    IsSuccess = false,
                    Message = ex.ToString()
                });
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            var blog = db.Blogs.FirstOrDefault(blog => blog.Blog_Id == id);
            if (blog is null)
            {
                return NotFound(new { IsSuccess = false, Message = "No data found!" });
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult PostBlogs(BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();

            BlogResponseModel model = new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving successful." : "Saving failed!",
                Data = blog
            };

            return Ok(model);
        }

        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok("Update blogs");
        }

        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok("Delete blogs");
        }

        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok("Patch blogs");
        }
    }
}
