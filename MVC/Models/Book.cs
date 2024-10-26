namespace MVC.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public double BookPrice { get; set; }
        public string BookImage { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}