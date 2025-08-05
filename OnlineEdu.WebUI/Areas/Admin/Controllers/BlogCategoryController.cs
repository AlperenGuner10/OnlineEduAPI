using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogCategoryDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Validators;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class BlogCategoryController : Controller
	{
		private readonly HttpClient _httpClient;

		public BlogCategoryController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
			return View(values);
		}

		public async Task<IActionResult> DeleteBlogCategory(int id)
		{
			await _httpClient.DeleteAsync($"BlogCategories/{id}");
			return RedirectToAction("Index");
		}

		public IActionResult CreateBlogCategory()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto createBlogCategoryDto)
		{
			var validator = new BlogCategoryValidator();
			var result = await validator.ValidateAsync(createBlogCategoryDto);

			if (!result.IsValid)
			{
				ModelState.Clear();
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
				return View();
			}

			await _httpClient.PostAsJsonAsync("BlogCategories",createBlogCategoryDto);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> UpdateBlogCategory(int id)
		{
			var values = await _httpClient.GetFromJsonAsync<UpdateBlogCategoryDto>($"BlogCategories/{id}");
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBlogCategory(UpdateBlogCategoryDto updateBlogCategoryDto)
		{
			await _httpClient.PutAsJsonAsync($"BlogCategories", updateBlogCategoryDto);
			return RedirectToAction("Index");
		}
	}
}
