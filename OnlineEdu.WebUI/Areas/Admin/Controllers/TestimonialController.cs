using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.TestimonialDTOs;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class TestimonialController : Controller
	{
		private readonly HttpClient _client = HttpClientInstance.CreateClient();
		public async Task<IActionResult> Index()
		{
			var values = await _client.GetFromJsonAsync<List<ResultTestimonialDto>>("testimonials");
			return View(values);
		}

		public async Task<IActionResult> DeleteTestimonial(int id)
		{
			await _client.DeleteAsync($"testimonials/{id}");
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult CreateTestimonial()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDTO)
		{
			await _client.PostAsJsonAsync("testimonials", createTestimonialDTO);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> UpdateTestimonial(int id)
		{
			var values = await _client.GetFromJsonAsync<UpdateTestimonialDto>($"testimonials/{id}");
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDTO)
		{
			await _client.PutAsJsonAsync("testimonials", updateTestimonialDTO);
			return RedirectToAction("Index");
		}
	}
}
