using System.Collections.Generic;

namespace MVC.Models
{
    //model to get user info
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }   
        public List<string> Roles { get; set; }
    }
}
