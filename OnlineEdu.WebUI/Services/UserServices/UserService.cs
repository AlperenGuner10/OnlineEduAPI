using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Models;

namespace OnlineEdu.WebUI.Services.UserServices
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;
		public UserService(IHttpClientFactory clientFactory)
		{
			_httpClient=clientFactory.CreateClient("EduClient");
		}

		public Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto)
		{
			throw new NotImplementedException();
		}

		public Task<bool> CreateRoleAsync(UserRoleDto userRoleDto)
		{
			throw new NotImplementedException();
		}


		public async Task<List<ResultUserDto>> GetFourTeachers()
		{
			throw new NotImplementedException();
		}

		public async Task<List<UserViewModel>> GetAllUserAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<UserViewModel>>("roleAssigns");
		}


		public async Task<string> LoginAsync(UserLoginDto userLoginDto)
		{
			throw new NotImplementedException();

		}

		public async Task LogoutAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<int> GetTeacherCount()
		{
			throw new NotImplementedException();
		}

		public async Task<List<ResultUserDto>> GetAllTeachers()
		{
			throw new NotImplementedException();
		}

		public async Task<List<UserViewModel>> GetAllUsersAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<UserViewModel>>("roleAssigns");
		}
		public async Task<List<AssignRoleDto>> GetUserForRoleAssign(int id)
		{
			return await _httpClient.GetFromJsonAsync<List<AssignRoleDto>>("roleAssigns/"+id);
		}

		public Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto)
		{
			throw new NotImplementedException();
		}
	}
}
