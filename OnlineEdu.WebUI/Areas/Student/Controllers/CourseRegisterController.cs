using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.DTOs.CourseRegisterDTOs;
using OnlineEdu.WebUI.DTOs.CourseVideoDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;

namespace OnlineEdu.WebUI.Areas.Student.Controllers
{
	[Area("Student")]
	[Authorize(Roles = "Student")]
	public class CourseRegisterController : Controller
	{
		private readonly ITokenService _tokenService;
		private readonly HttpClient _httpClient;
		public CourseRegisterController(IHttpClientFactory clientFactory, ITokenService tokenService)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
			_tokenService = tokenService;	
		}
		public async Task<IActionResult> Index()
		{
			var userId = _tokenService.GetUserId;
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseRegisterDto>>("courseRegisters/GetMyCourses/"+userId);
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

			var userId = _tokenService.GetUserId;
			courseRegisterDto.AppUserId = userId;
			var result = await _httpClient.PostAsJsonAsync("courseRegisters", courseRegisterDto);

			if(result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View(courseRegisterDto);
		}

		public async Task<IActionResult> CourseVideos(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseVideoDto>>("courseVideos/GetCourseVideosByCourseId/"+id);
			ViewBag.courseName = values.Select(x => x.Course.CourseName).FirstOrDefault();
			return View(values);
		}
	}
}
