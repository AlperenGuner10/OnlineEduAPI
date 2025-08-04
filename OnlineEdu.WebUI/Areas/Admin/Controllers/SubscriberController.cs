using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.SubscriberDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class SubscriberController : Controller
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultSubscriberDto>>("subscribers");
			return View(values);
		}

		public async Task<IActionResult> DeleteSubscriber(int id)
		{
			await _httpClient.DeleteAsync($"subscribers/{id}");
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ChangeStatusSubscriber(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<UpdateSubscriberDto>($"subscribers/{id}");
			if (values.IsActive)
			{
				values.IsActive = false;
			}
			else
				values.IsActive = true;

			await _httpClient.PutAsJsonAsync("subscribers", values);

			return RedirectToAction("Index");
		}

	}
}
