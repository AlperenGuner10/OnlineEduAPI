using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.ViewComponents.Blog
{
	public class _BlogRecentBlogs : ViewComponent
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultBlogDto>>("blogs/GetLastFourBlogs");
			return View(values);
		}
	}
}
