using Laboratory_Schedule.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Laboratory_Schedule.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> CreateRoles()
        {
            //check
            var roles = new[] { "Admin", "Recep" };
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole(role));

            }
            return View("index", "roles added successfully");
        }
        public async Task<IActionResult> AddRoleToUsers()
        {
            var arwa = await _userManager.FindByNameAsync("arwa@gmail.com");
            await _userManager.AddToRoleAsync(arwa, "Admin");

            var ahmad = await _userManager.FindByNameAsync("ahmad@gmail.com");
            await _userManager.AddToRoleAsync(ahmad, "Recep");

            return View("index", "users added successfully");
        }

        public async Task <IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}