using Azure;
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

        [HttpGet]
        public IActionResult AddBlog() {
            return View();
        }



        [HttpPost]
        public IActionResult AddBlog(AddBlogRequest addBlogRequest)
        {
            var newBlog = new BlogPost
            {
                Heading = addBlogRequest.Heading,
                PageTitle = addBlogRequest.PageTitle,
                Content = addBlogRequest.Content,
                ShortDescription = addBlogRequest.ShortDescription,
                FeaturedImageUrl = addBlogRequest.FeaturedImageUrl,
                UrlHandle = addBlogRequest.UrlHandle,
                PublishedDate = DateTime.UtcNow,
                Author = addBlogRequest.Author,
                Visible = true,
                Tags = addBlogRequest.Tags
            };

            _bloggerDbContext.BlogPosts.Add(newBlog);
            _bloggerDbContext.SaveChanges();

            return View("AddBlog");
        }
    }
}
