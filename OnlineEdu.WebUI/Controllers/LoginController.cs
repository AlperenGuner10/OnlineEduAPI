using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Services.UserServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService=userService;
		}

		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
		{
			var userRole = await _userService.LoginAsync(userLoginDto);

			if (userRole == "Admin")
			{
				return RedirectToAction("Index", "About", new { area = "Admin" });
			}
			if (userRole == "Teacher")
			{
				return RedirectToAction("Index", "MyCourse", new { area = "Teacher" });
			}
			if (userRole == "Student")
			{
				return RedirectToAction("Index", "CourseRegister", new { area = "Student" });
			}
			else
			{
				ModelState.AddModelError("", "E-Mail veya Şifre hatalı.");
				return View();
			}
		}
	}
}
