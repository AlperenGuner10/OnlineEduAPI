using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Services.UserServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IUserService _userService;

		public RegisterController(IUserService userService)
		{
			_userService=userService;
		}

		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
		{
			
			if(!ModelState.IsValid)
			{
				return View(userRegisterDto);
			}

			var result = await _userService.CreateUserAsync(userRegisterDto);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
				return View(userRegisterDto);
			}
			return RedirectToAction("Index","Login");
		}
	}
}
