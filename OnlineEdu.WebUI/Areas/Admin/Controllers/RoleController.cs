using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.RoleDTOs;
using OnlineEdu.WebUI.Helpers;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class RoleController : Controller
	{
		private readonly HttpClient _httpClient;

		public RoleController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}

		public async Task<IActionResult> Index()
		{
			var values = await _httpClient.GetFromJsonAsync<List<ResultRoleDto>>("roles");
			return View(values);
		}

		public IActionResult CreateRole()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
		{
			await _httpClient.PostAsJsonAsync("roles", createRoleDto);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DeleteRole(int id)
		{
			await _httpClient.DeleteAsync("roles/"+id);
			return RedirectToAction("Index");
		}
	}
}
