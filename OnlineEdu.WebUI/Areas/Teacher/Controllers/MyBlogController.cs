using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.BlogCategoryDTOs;
using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
	[Area("Teacher")]
	public class MyBlogController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly ITokenService _tokenService;
		public MyBlogController(IHttpClientFactory clientFactory, ITokenService tokenService)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
			_tokenService = tokenService;
		}

		public async Task<IActionResult> Index()
		{
			var userId = _tokenService.GetUserId;
			var values = await _httpClient.GetFromJsonAsync<List<ResultBlogDto>>("blogs/GetBlogByWriterId/"+userId);

			return View(values);
		}

		public async Task BlogCategoryDropDownAsync()
		{
			var categoryList = await _httpClient.GetFromJsonAsync<List<ResultBlogCategoryDto>>("blogCategories");

			List<SelectListItem> categories = (from x in categoryList
											   select new SelectListItem
											   {
												   Text = x.Name,
												   Value = x.BlogCategoryId.ToString()
											   }).ToList();

			ViewBag.categories = categories;
		}

		public async Task<IActionResult> CreateBlog()
		{	
			await BlogCategoryDropDownAsync();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
		{
			var userId = _tokenService.GetUserId;
			createBlogDto.WriterId=userId;
			await _httpClient.PostAsJsonAsync("blogs", createBlogDto);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> UpdateBlog(int id)
		{
			await BlogCategoryDropDownAsync();
			var value = await _httpClient.GetFromJsonAsync<UpdateBlogDto>("blogs/"+id);
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
		{
			await _httpClient.PutAsJsonAsync("blogs", updateBlogDto);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteBlog(int id)
		{
			await _httpClient.DeleteAsync("blogs/"+id);
			return RedirectToAction("Index");
		}
	}
}
