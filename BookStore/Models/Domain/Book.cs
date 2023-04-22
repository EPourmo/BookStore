using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Isbn { get; set; } = null!;
        [Required]
        public int TotalPage { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int GenreId { get; set; }


    }
}
