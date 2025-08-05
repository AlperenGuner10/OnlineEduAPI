using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeBlogComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _HomeBlogComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultBlogDto>>("blogs/GetLastFourBlogs");
			return View(values);
		}
	}
}
