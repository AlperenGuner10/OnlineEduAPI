using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TeacherListController : Controller
	{
		private readonly HttpClient _httpClient;

		public TeacherListController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public async Task<IActionResult> Index()
		{
			var teachers = await _httpClient.GetFromJsonAsync<List<ResultUserDto>>("users/TeacherList");
			return View(teachers);
		}
	}
}

