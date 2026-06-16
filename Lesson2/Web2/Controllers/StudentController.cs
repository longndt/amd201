using Microsoft.AspNetCore.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            //create new student
            Student s1 = new Student("Tuan Hung", 20, 7.8, "https://stradaeducation.org/wp-content/uploads/2018/01/crisis-scaled-1.jpeg");

            //pass data to view using Model
            return View(s1);
        }

        public IActionResult StudentList()
        {
            //create list of students
            List<Student> students = new List<Student>()
            {
                new Student
                {
                    Name = "Phuong Linh",
                    Age = 23,
                    Grade = 9.4,
                    Image = "https://media.istockphoto.com/id/1365601848/photo/portrait-of-a-young-woman-carrying-her-schoolbooks-outside-at-college.jpg?s=612x612&w=0&k=20&c=EVxLUZsL0ueYFF1Nixit6hg-DkiV52ddGw_orw9BSJA="
                },
                new Student
                {
                    Name = "Tien Hung",
                    Age = 21,
                    Grade = 8.4,
                    Image = "https://media.istockphoto.com/id/1336063208/photo/single-portrait-of-smiling-confident-male-student-teenager-looking-at-camera-in-library.jpg?s=612x612&w=0&k=20&c=w9SCRRCFa-Li82fmZotJzHdGGWXBVN7FgfBCD5NK7ic="
                }
            };
            Student newStudent = new Student("Tuan", 20, 8.5, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLszadvHS34cJQyMwulHvwZAffQFCerxSkpg&s");
            Student anotherStudent = new Student
            {
                Name = "Hoa",
                Age = 22,
                Grade = 6.8,
                Image = "https://img.freepik.com/free-photo/happy-young-female-student-holding-notebooks-from-courses-smiling-camera-standing-spring-clothes-against-blue-background_1258-70161.jpg"
            };
            //add new item (object) to list
            students.Add(newStudent);
            students.Add(anotherStudent);

            //pass list to array using model
            return View(students);
        }
    }
}
