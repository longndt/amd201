namespace Tutorial1.Models
{
    //class name => table name
    public class Laptop
    {
        //property name => column name
        public int Id { get; set; }         //primary key, auto increment
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
