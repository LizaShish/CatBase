namespace CatBase.Models
{
    public class Cat
    {
        public Guid Id { get; set; }
        public string CatsName { get; set; }

        public decimal Age { get; set; }
        public string Breeds { get; set; } 
        public string Gender { get; set; }

        public string Img { get; set; }



    }
}
