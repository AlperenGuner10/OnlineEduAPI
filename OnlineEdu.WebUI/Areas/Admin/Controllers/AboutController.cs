using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.AboutDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("[area]/[controller]/[action]/{id?}")]
	public class AboutController : Controller
	{
		private readonly HttpClient _client = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var values = await _client.GetFromJsonAsync<List<ResultAboutDto>>("abouts");
			return View(values);
		}

		public async Task<IActionResult> DeleteAbout(int id)
		{
			await _client.DeleteAsync($"abouts/{id}");
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
			await _client.PostAsJsonAsync("abouts", createAboutDTO);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateAbout(int id)
		{
			var values = await _client.GetFromJsonAsync<UpdateAboutDTO>($"abouts/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
		{
			await _client.PutAsJsonAsync("abouts", updateAboutDTO);
			return RedirectToAction("Index");
		}
	}
}
