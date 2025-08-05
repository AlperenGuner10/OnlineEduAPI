using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.SocialMediaDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class SocialMediaController : Controller
	{
		private readonly HttpClient _httpClient;

		public SocialMediaController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultSocialMediaDto>>("SocialMedias");
			return View(values);
		}

		public async Task<IActionResult> DeleteSocialMedia(int id)
		{
			await _httpClient.DeleteAsync($"SocialMedias/{id}");
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult CreateSocialMedia()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
		{
			await _httpClient.PostAsJsonAsync("SocialMedias", createSocialMediaDto);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateSocialMedia(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<UpdateSocialMediaDto>($"SocialMedias/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
		{
			await _httpClient.PutAsJsonAsync("SocialMedias", updateSocialMediaDto);
			return RedirectToAction("Index");
		}
	}
}
