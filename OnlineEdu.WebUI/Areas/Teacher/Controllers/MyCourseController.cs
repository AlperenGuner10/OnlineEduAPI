using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.CourseCategoryDTOs;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
	[Authorize(Roles = "Teacher")]
	[Area("Teacher")]
	public class MyCourseController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		private readonly UserManager<AppUser> _userManager;

		public MyCourseController(UserManager<AppUser> userManager)
		{
			_userManager=userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course/GetCoursesByTeacherId/"+user.Id);

			return View(values);
		}

		public async Task<IActionResult> CreateCourse()
		{
			var categoryList = await _httpClient.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories");

			List<SelectListItem> categories = (from x in categoryList
											   select new SelectListItem
											   {
												   Text =x.Name,
												   Value = x.CourseCategoryId.ToString()
											   }).ToList();
			ViewBag.categories = categories;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			createCourseDto.AppUserId = user.Id;
			createCourseDto.IsShown = false;

			await _httpClient.PostAsJsonAsync("course", createCourseDto);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteCourse(int id)
		{
			await _httpClient.DeleteAsync("course/"+id);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> UpdateCourse(int id)
		{
			var categoryList = await _httpClient.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories");

			List<SelectListItem> categories = (from x in categoryList
											   select new SelectListItem
											   {
												   Text =x.Name,
												   Value = x.CourseCategoryId.ToString()
											   }).ToList();
			ViewBag.categories = categories;
			var value = await _httpClient.GetFromJsonAsync<UpdateCourseDto>("course/GetCoursesByTeacherId/"+id);
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			updateCourseDto.AppUserId = user.Id;

			await _httpClient.PutAsJsonAsync("course", updateCourseDto);
			return RedirectToAction("Index");
		}
	}
}
