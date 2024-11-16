namespace WebAPI.Models
{
    //model Book => table Book
    public class Book
    {
        //property name => column name
        public int Id { get; set; } //primary key, auto increment
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
