using CatBase.Models;

namespace CatBase.DTOs
{
    public class CatDTO
    {
        public Guid Id { get; set; }
        public string CatsName { get; set; }
        public string Breeds { get; set; }
        public required List<Cat> Cats { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public required string SearchTerm { get; set; }

        public int TotalPages { get; set; }
    }
}
