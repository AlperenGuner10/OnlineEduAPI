using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.CourseCategoryDTOs;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("[area]/[controller]/[action]/{id?}")]
	public class CourseController : Controller
	{
		private readonly HttpClient _client = HttpClientInstance.CreateClient();

		public async Task CourseCategoryDropdown()
		{
			var courseCategoryList = await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories");

			List<SelectListItem> courseCategories = (from x in courseCategoryList
													 select new SelectListItem
													 {
														 Text = x.Name,
														 Value = x.CourseCategoryId.ToString()
													 }).ToList();
			ViewBag.courseCategories = courseCategories;
		}

		public async Task<IActionResult> Index()
		{
			var values = await _client.GetFromJsonAsync<List<ResultCourseDto>>("Course");
			return View(values);
		}

		public async Task<IActionResult> DeleteCourse(int id)
		{
			await _client.DeleteAsync($"Course/{id}");
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> CreateCourse()
		{
			await CourseCategoryDropdown();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
		{
			await _client.PostAsJsonAsync("Course", createCourseDto);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateCourse(int id)
		{
			await CourseCategoryDropdown();
			var values = await _client.GetFromJsonAsync<UpdateCourseDto>($"Course/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
		{
			await _client.PutAsJsonAsync("Course", updateCourseDto);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ShowOnHome(int id)
		{
			await _client.GetAsync("Course/ShowOnHome/"+id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> DontShowOnHome(int id)
		{
			await _client.GetAsync("Course/DontShowOnHome/"+id);
			return RedirectToAction("Index");
		}
	}
}
