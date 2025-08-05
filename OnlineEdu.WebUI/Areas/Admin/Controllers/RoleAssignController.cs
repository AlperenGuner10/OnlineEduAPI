using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Models;
using OnlineEdu.WebUI.Services.UserServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class RoleAssignController : Controller
	{
		private readonly IUserService _userService;
		private readonly HttpClient _httpClient;
		public RoleAssignController(IUserService userService, IHttpClientFactory clientFactory)
		{
			_userService=userService;
			_httpClient=clientFactory.CreateClient("EduClient");
		}

		public async Task<IActionResult> Index()
		{
			var values = await _userService.GetAllUsersAsync();
			return View(values);
		}

		[HttpGet]
		public async Task<IActionResult> AssignRole(int id)
		{
			var values = await _userService.GetUserForRoleAssign(id);
			return View(values);
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(List<AssignRoleDto> assignRoleList)
		{
			var result = await _httpClient.PostAsJsonAsync("roleAssigns", assignRoleList);
			if (result.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View(assignRoleList);
		}
	}
}
