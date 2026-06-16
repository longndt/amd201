namespace Web2.Models
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public string Image { get; set; }

        public Student () { }

        public Student (string name, int age, double grade, string image)
        {
            Name = name;
            Age = age;
            Grade = grade;
            Image = image;
        }

        //private string name;

        //public string getName() {
        //    return name;
        //}

        //public void setName (string newName)
        //{
        //    name = newName;
        //}
    }
}
