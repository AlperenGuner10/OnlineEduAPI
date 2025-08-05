using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.TeacherSocialDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
	[Area("Teacher")]
	[Authorize(Roles ="Teacher")]
	public class MySocialMediaController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly ITokenService _tokenService;
		public MySocialMediaController(IHttpClientFactory clientFactory, ITokenService tokenService)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
			_tokenService = tokenService;
		}

		public async Task<IActionResult> Index()
		{
			var userId = _tokenService.GetUserId;
			var values = await _httpClient.GetFromJsonAsync<List<ResultTeacherSocialDto>>("teacherSocials/byTeacherId/"+userId);
			return View(values);
		}
		public async Task<IActionResult> DeleteTeacherSocial(int id)
		{
			await _httpClient.DeleteAsync($"teacherSocials/{id}");
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult CreateTeacherSocial()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateTeacherSocial(CreateTeacherSocialDto createTeacherSocialDto)
		{
			var userId = _tokenService.GetUserId;
			createTeacherSocialDto.TeacherId = userId;
			await _httpClient.PostAsJsonAsync("teacherSocials", createTeacherSocialDto);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateTeacherSocial(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<UpdateTeacherSocialDto>($"teacherSocials/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateTeacherSocial(UpdateTeacherSocialDto updateTeacherSocialDto)
		{
			await _httpClient.PutAsJsonAsync("teacherSocials", updateTeacherSocialDto);
			return RedirectToAction("Index");
		}
	}
}
