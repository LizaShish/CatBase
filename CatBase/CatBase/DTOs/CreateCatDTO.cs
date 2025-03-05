using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatBase.DTOs
{
    public class CreateCatDTO
    {
        public Guid Id { get; set; }
        public string CatsName { get; set; }
        public int Age { get; set; }
        public string Breeds { get; set; }
        public string Gender { get; set; }
        public string Img { get; set; }

        public List<SelectListItem> BreedsList { get; set; } = new();
    }
}
