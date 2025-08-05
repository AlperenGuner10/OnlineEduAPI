using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.AboutDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles ="Admin")]
	[Area("Admin")]
	public class AboutController : Controller
	{
		private readonly HttpClient _httpClient;

		public AboutController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultAboutDto>>("abouts");
			return View(values);
		}

		public async Task<IActionResult> DeleteAbout(int id)
		{
			await _httpClient.DeleteAsync($"abouts/{id}");
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult CreateAbout()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
		{
			await _httpClient.PostAsJsonAsync("abouts", createAboutDTO);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateAbout(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<UpdateAboutDTO>($"abouts/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
		{
			await _httpClient.PutAsJsonAsync("abouts", updateAboutDTO);
			return RedirectToAction("Index");
		}
	}
}
