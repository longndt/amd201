using System.ComponentModel.DataAnnotations;

namespace Tutorial1.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Tiêu đề sách không được để trống")]
        public string Title { get; set; }

        [MinLength(3, ErrorMessage = "Tên tác giả tối thiểu 3 ký tự")]
        [MaxLength(15, ErrorMessage = "Tên tác giả tối đa 15 ký tự")]
        public string Author { get; set; }
    }
}
