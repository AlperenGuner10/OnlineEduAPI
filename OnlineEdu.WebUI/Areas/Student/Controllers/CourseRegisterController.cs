using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.DTOs.CourseRegisterDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Student.Controllers
{
	[Area("Student")]
	[Authorize(Roles = "Student")]
	public class CourseRegisterController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		private readonly UserManager<AppUser> _userManager;

		public CourseRegisterController(UserManager<AppUser> userManager)
		{
			_userManager=userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseRegisterDto>>("courseRegisters/GetMyCourses/"+user.Id);
			return View(values);
		}
		[HttpGet]
		public async Task<IActionResult> RegisterCourse()
		{
			var courseList = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course");

			List<SelectListItem> courses = (from x in courseList
											select new SelectListItem
											{
												Text = x.CourseName,
												Value = x.CourseId.ToString()
											}).ToList();
			ViewBag.courses = courses;

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RegisterCourse(CreateCourseRegisterDto courseRegisterDto)
		{
			var courseList = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course");

			List<SelectListItem> courses = (from x in courseList
							   select new SelectListItem
							   {
								   Text = x.CourseName,
 								   Value = x.CourseId.ToString()
							   }).ToList();
			ViewBag.courses = courses;

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			courseRegisterDto.AppUserId = user.Id;
			var result = await _httpClient.PostAsJsonAsync("courseRegisters", courseRegisterDto);

			if(result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View(courseRegisterDto);
		}
	}
}
