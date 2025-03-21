using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CatBase.DTOs
{
    public class CreateCatDTO
    {
        public Guid Id { get; set; }
        public string CatsName { get; set; }

        [Range(0, 100, ErrorMessage = "Возраст должен быть положительным числом и не превышать 100 лет.")]
        public decimal Age { get; set; }
        public string Breeds { get; set; }
        public string Gender { get; set; }
        public string Img { get; set; }

        public List<SelectListItem> BreedsList { get; set; } = new();
    }
}
