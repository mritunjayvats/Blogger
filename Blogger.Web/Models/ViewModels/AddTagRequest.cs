using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blogger.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
