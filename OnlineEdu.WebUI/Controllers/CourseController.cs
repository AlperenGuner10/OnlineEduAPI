using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Controllers
{
	public class CourseController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var courses = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course");
			return View(courses);
		}

		public async Task<IActionResult> GetCoursesByCategoryId(int id)
		{
			var courses = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course/GetCoursesByCategoryId/"+id);

			var categoryName = (from x in courses
								select x.Category.Name).FirstOrDefault();
			ViewBag.category = categoryName;
			return View(courses);
		}
	}
}
