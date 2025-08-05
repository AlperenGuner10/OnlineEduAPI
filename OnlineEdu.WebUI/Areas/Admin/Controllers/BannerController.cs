using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BannerDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class BannerController : Controller
	{
		private readonly HttpClient _client;

		public BannerController(IHttpClientFactory clientFactory)
		{
			_client=clientFactory.CreateClient("EduClient");
		}
		public async Task<IActionResult> Index()
		{
			var values = await _client.GetFromJsonAsync<List<ResultBannerDto>>("banners");
			return View(values);
		}

		public async Task<IActionResult> DeleteBanner(int id)
		{
			await _client.DeleteAsync($"banners/{id}");
			return RedirectToAction("Index");
		}

		public IActionResult CreateBanner()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBanner(CreateBannerDTO createBannerDTO)
		{
			await _client.PostAsJsonAsync("banners", createBannerDTO);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> UpdateBanner(int id)
		{
			var values = await _client.GetFromJsonAsync<UpdateBannerDTO>($"banners/{id}");
			return View(values);
		}

		[HttpPost]

		public async Task<IActionResult> UpdateBanner(UpdateBannerDTO updateBannerDTO)
		{
			await _client.PutAsJsonAsync("abouts", updateBannerDTO);
			return RedirectToAction("Index");
		}
	}
}
