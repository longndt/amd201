using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }

        [Range(5, 500, ErrorMessage = "Giá tiền phải từ 5$ đến 500$")]
        public double BookPrice { get; set; }

        //[Url(ErrorMessage = "Link ảnh không hợp lệ")]
        public string BookImage { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}