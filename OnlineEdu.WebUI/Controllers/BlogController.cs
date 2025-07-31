using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.DTOs.SubscriberDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Controllers
{
	public class BlogController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Subscribe(CreateSubscriberDto createSubscriberDto)
		{
			await _httpClient.PostAsJsonAsync("subscribers",createSubscriberDto);
			return NoContent();
		}

		public async Task<IActionResult> BlogDetails(int id)
		{
			var blog = await _httpClient.GetFromJsonAsync<ResultBlogDto>("blogs/"+id);
			return View(blog);
		}
	}
}
