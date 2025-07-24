using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BannerDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeBannerComponent : ViewComponent
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultBannerDto>>("banners");
			return View(values);
		}
	}
}
