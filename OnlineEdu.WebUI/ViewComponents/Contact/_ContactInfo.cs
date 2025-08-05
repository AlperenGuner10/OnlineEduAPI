using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.ContactDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.ViewComponents.Contact
{
	public class _ContactInfo : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _ContactInfo(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultContactDto>>("contacts");
			return View(values);
		}
	}
}
