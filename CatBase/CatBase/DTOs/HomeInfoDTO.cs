using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatBase.DTOs
{
    public class HomeInfoDTO
    {

        public string ProjectName { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; }

        public List<SelectListItem> Facts { get; set; } = new();

    }
}
