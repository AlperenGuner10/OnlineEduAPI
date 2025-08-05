using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class StudentListController : Controller
	{
		private readonly HttpClient _httpClient;

		public StudentListController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}

		public async Task<IActionResult> Index()
		{
			var students = await _httpClient.GetFromJsonAsync<List<ResultUserDto>>("users/StudentList");
			return View(students);
		}
	}
}
