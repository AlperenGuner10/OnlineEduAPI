using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.ContactDTOs;
using OnlineEdu.WebUI.DTOs.MessageDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Controllers
{
	public class ContactController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultContactDto>>("contacts");
			ViewBag.map = values.Select(x => x.MapUrl).FirstOrDefault();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
		{
			await _httpClient.PostAsJsonAsync("messages",createMessageDto);
			return NoContent();
		}
	}
}
