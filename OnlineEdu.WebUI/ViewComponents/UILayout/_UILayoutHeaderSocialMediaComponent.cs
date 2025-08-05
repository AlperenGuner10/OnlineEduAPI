using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.SocialMediaDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.ViewComponents.UILayout
{
	public class _UILayoutHeaderSocialMediaComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _UILayoutHeaderSocialMediaComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultSocialMediaDto>>("socialMedias");
			return View(values);
		}
	}
}
