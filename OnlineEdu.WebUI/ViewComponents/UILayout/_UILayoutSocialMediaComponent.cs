using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.SocialMediaDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.UILayout
{
	public class _UILayoutSocialMediaComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _UILayoutSocialMediaComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var socialMedia = await _httpClient.GetFromJsonAsync<List<ResultSocialMediaDto>>("socialMedias");
			return View(socialMedia);
		}
	}
}
