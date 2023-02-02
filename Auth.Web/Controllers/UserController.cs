using Auth.Web.Data;
using Auth.Web.Models;
using Auth.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<User> _userManager;

        public UserController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var users = _db.Users.Where(x => !x.IsDelete).Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CreatedAt = x.CreatedAt
            }).ToList();
          

            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateUserViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.CreatedAt = DateTime.Now;
                user.UserName = input.Email;
                user.Email = input.Email;
                user.PhoneNumber = input.PhoneNumber;
                await _userManager.CreateAsync(user, input.Password);
                return RedirectToAction("Index");
            }
            return View(input);
        }

        public IActionResult Delete(string Id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == Id && !x.IsDelete);
            user.IsDelete = true;
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}
