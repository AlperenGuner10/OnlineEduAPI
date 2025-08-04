using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Entity.Entities;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class StudentListController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public StudentListController(UserManager<AppUser> userManager)
		{
			_userManager=userManager;
		}

		public async Task<IActionResult> Index()
		{
			var students = await _userManager.GetUsersInRoleAsync("Student");
			return View(students);
		}
	}
}
