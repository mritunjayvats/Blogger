using Blogger.Web.Data;
using Blogger.Web.Models.Domain;
using Blogger.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var newTag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _bloggerDbContext.Tags.AddAsync(newTag);
            await _bloggerDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var List = await _bloggerDbContext.Tags.ToListAsync();

            return View(List);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _bloggerDbContext.Tags.SingleOrDefaultAsync(x => x.Id == id);

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await _bloggerDbContext.Tags.FindAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _bloggerDbContext.SaveChangesAsync();
                return RedirectToAction("List");

            }

            return View("Edit" , new {id = tag.Id } );
        }
        
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tag = await _bloggerDbContext.Tags.FindAsync(id);

            if(tag != null)
            {
                _bloggerDbContext.Tags.Remove(tag);
                await _bloggerDbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = await _bloggerDbContext.Tags.FindAsync(editTagRequest.Id);

            if(tag != null )
            {
                _bloggerDbContext.Tags.Remove(tag);
                await _bloggerDbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

    }
}
