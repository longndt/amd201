using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }
            return View(userList);
        }

        public async Task<IActionResult> ResetPass(string id)
        {
            //find user in DB with given ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found !";
                return RedirectToAction("UserList");
            }

            //Generate new password 
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var newPass = "123@Abc";  //set custom password here

            //Reset password
            var result = await _userManager.ResetPasswordAsync(user, token, newPass);
            
            //Return result
            if (result.Succeeded)
            {
                TempData["Message"] = $"Reset password succeed for email {user.Email} to default pass: {newPass}";
            } 
            else
            {
                TempData["Error"] = $"Reset password failed for email {user.Email}";
            }
 
            return RedirectToAction("UserList");
        }
    }
}
