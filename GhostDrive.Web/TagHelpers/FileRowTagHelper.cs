using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GhostDrive.Web.TagHelpers
{
    public class FileRowTagHelper : TagHelper
    {
        public string FileId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tr";
            var href = $"/Files/Details/{FileId}";
            output.Attributes.SetAttribute("onclick", $"window.location='{href}';");
            var target = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(target.GetContent());
        }
    }
}
