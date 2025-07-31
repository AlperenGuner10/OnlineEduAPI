using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.TestimonialDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
	public class _HomeTestimonialComponent : ViewComponent
	{
		private readonly HttpClient _httpClient = HttpClientInstance.CreateClient();
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultTestimonialDto>>("testimonials");
			return View(values);
		}
	}
}
