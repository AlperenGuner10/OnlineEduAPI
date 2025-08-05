using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.CourseCategoryDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeCourseCategoryComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _HomeCourseCategoryComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories/GetActiveCategories");
			return View(values);
		}
	}
}
