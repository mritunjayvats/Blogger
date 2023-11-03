using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggerDbContext _bloggerDbContext;

        public AdminTagsController(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            var newTag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
               
            };

            _bloggerDbContext.Tags.Add(newTag);
            _bloggerDbContext.SaveChanges();

            return View("Add");
        }
    }
}
