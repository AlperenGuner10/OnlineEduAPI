using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.Controllers
{
	public class RegisterController : Controller
	{
		private readonly HttpClient _httpClient;

		public RegisterController(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
		{
			
			if(!ModelState.IsValid)
			{
				return View(userRegisterDto);
			}

			var result = await _httpClient.PostAsJsonAsync("users/register",userRegisterDto);
			if (!result.IsSuccessStatusCode)
			{
				var errors = await result.Content.ReadFromJsonAsync<List<RegisterResponseDto>>();
				foreach (var item in errors)
				{
					ModelState.AddModelError("", item.Description);
				}
				return View(userRegisterDto);
			}
			return RedirectToAction("SignIn", "Login");
		}
	}
}
