using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.CourseCategoryDTOs;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.DTOs.CourseVideoDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
	[Authorize(Roles = "Teacher")]
	[Area("Teacher")]
	public class MyCourseController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly ITokenService _tokenService;
		public MyCourseController(IHttpClientFactory clientFactory, ITokenService tokenService)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
			_tokenService = tokenService;
		}

		public async Task<IActionResult> Index()
		{
			var userId = _tokenService.GetUserId;

			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course/GetCoursesByTeacherId/"+ userId);

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
			var userId = _tokenService.GetUserId;

			createCourseDto.AppUserId = userId;
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
			var userId = _tokenService.GetUserId;
			updateCourseDto.AppUserId = userId;

			await _httpClient.PutAsJsonAsync("course", updateCourseDto);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> CourseVideos(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseVideoDto>>("courseVideos/GetCourseVideosByCourseId/"+id);

			TempData["courseId"] =id;
			ViewBag.courseName = values.Select(x => x.Course.CourseName).FirstOrDefault();
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> CreateVideo()
		{
			var courseId = (int)TempData["courseId"];
			var course = await _httpClient.GetFromJsonAsync<ResultCourseDto>("course/"+courseId);
			ViewBag.courseName = course.CourseName;
			ViewBag.courseId = course.CourseId;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateVideo(CreateCourseVideoDto createCourseVideoDto)
		{
			await _httpClient.PostAsJsonAsync("courseVideos", createCourseVideoDto);
			return RedirectToAction("Index");
		}
	}
}
