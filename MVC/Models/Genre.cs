using System.Collections;
using System.Collections.Generic;

namespace MVC.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
