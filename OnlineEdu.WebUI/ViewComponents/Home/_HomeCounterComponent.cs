using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeCounterComponent : ViewComponent
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		private readonly IUserService _userService;

		public _HomeCounterComponent(IUserService userService)
		{
			_userService=userService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewBag.blogCount = await _httpClient.GetFromJsonAsync<int>("blogs/GetBlogCount");
			ViewBag.courseCount = await _httpClient.GetFromJsonAsync<int>("course/GetCourseCount");
			ViewBag.courseCategoryCount = await _httpClient.GetFromJsonAsync<int>("courseCategories/GetCourseCategoryCount");
			ViewBag.teacherCount = await _userService.GetTeacherCount();
			return View();
		}
	}
}
