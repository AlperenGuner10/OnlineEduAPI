using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.AboutDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Controllers
{
	public class AboutController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var value = await _httpClient.GetFromJsonAsync<List<ResultAboutDto>>("abouts");
			return View(value);
		}
	}
}
