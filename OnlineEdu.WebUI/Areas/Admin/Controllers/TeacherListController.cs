using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TeacherListController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public TeacherListController(UserManager<AppUser> userManager)
		{
			_userManager=userManager;
		}
		public async Task<IActionResult> Index()
		{
			var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
			return View(teachers);
		}
	}
}

