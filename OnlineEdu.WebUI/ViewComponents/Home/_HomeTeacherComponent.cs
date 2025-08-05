using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeTeacherComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _HomeTeacherComponent(IHttpClientFactory clientFactory)
		{
			_httpClient = clientFactory.CreateClient("EduClient");
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultUserDto>>("users/GetFourTeachers");
			return View(values);
		}
	}
}
