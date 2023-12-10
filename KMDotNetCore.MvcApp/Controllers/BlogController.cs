using KMDotNetCore.MvcApp.EFDbContext;
using KMDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            List<BlogDataModel> lst = _context.Blogs.ToList();

            return View("BlogIndex", lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving successful" : "Saving failed";
            TempData["Message"] = message;
            TempData["isSuccess"] = result > 0;

            // ViewBag
            // ViewData
            // TempData
            // Session
            return Redirect("/blog");
        }
    }
}
