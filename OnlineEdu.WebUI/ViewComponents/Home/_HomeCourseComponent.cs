using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeCourseComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _HomeCourseComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("course/GetActiveCourses");
			return View(values);
		}
	}
}
