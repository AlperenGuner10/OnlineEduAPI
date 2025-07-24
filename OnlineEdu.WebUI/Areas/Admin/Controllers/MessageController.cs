using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.MessageDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("[area]/[controller]/[action]/{id?}")]
	public class MessageController : Controller
	{
		private readonly HttpClient _client = HttpClientInstance.CreateClient();

		public async Task<IActionResult> Index()
		{
			var values = await _client.GetFromJsonAsync<List<ResultMessageDto>>("Messages");
			return View(values);
		}

		public async Task<IActionResult> MessageDetail(int id)
		{
			var values = await _client.GetFromJsonAsync<ResultMessageDto>($"Messages/{id}");
			return View(values);
		}
	}
}
