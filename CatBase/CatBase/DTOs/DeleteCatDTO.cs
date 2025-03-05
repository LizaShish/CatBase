using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatBase.DTOs
{
    public class DeleteCatDTO
    {
        public Guid Id { get; set; }
        public string CatsName { get; set; }
        public List<SelectListItem> CatsList { get; set; } = new List<SelectListItem>();
    }
}
