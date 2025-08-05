using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;

namespace OnlineEdu.WebUI.ViewComponents.Blog
{
	public class _BlogAllBlogs : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _BlogAllBlogs(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultBlogDto>>("blogs");
			return View(values);
		}
	}
}
