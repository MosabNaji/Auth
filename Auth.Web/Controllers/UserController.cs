using Auth.Web.Data;
using Auth.Web.Models;
using Auth.Web.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;

        public UserController(ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager , IMapper mapper )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            
            
        }
        public IActionResult Index()
        {
            var users = _db.Users.OrderByDescending(x =>x.CreatedAt).Where(x => !x.IsDelete).ToList();
            var usersVM = _mapper.Map<List<User>, List<UserViewModel>>(users); //بدي أحول من يوزر ليوزر فيو مودل و بعطيه النوعين
            return View(usersVM);
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
                var user = _mapper.Map<User>(input); //بدي أحول من يوزر فيو مودل ليوزر و بقدر أعطيه فقط النوع اللي بدي أحول الو
                 
                user.CreatedAt = DateTime.Now;
                user.UserName = input.Email;
               
                await _userManager.CreateAsync(user, input.Password);
                if(input.UserType == Enums.UserType.Admin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else if(input.UserType == Enums.UserType.Employee)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
                
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

        public async Task<IActionResult> InitRoles()
        {
            if (!_db.Roles.Any())
            {
                var roles = new List<string>();
                roles.Add("Admin");
                roles.Add("Employee");
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
  
            return RedirectToAction("Index");
        }
    }
}
