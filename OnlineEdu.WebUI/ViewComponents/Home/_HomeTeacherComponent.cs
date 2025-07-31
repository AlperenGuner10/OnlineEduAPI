using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeTeacherComponent : ViewComponent
	{
		private readonly IUserService _userService;

		public _HomeTeacherComponent(IUserService userService)
		{
			_userService=userService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _userService.GetFourTeachers();
			return View(values);
		}
	}
}
