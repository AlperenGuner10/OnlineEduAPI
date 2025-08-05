using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeCounterComponent : ViewComponent
	{
		private readonly HttpClient _httpClient;
		public _HomeCounterComponent(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ViewBag.blogCount = await _httpClient.GetFromJsonAsync<int>("blogs/GetBlogCount");
			ViewBag.courseCount = await _httpClient.GetFromJsonAsync<int>("course/GetCourseCount");
			ViewBag.courseCategoryCount = await _httpClient.GetFromJsonAsync<int>("courseCategories/GetCourseCategoryCount");
			ViewBag.testimonialCount = await _httpClient.GetFromJsonAsync<int>("testimonials/GetTestimonialCount");
			return View();
		}
	}
}
