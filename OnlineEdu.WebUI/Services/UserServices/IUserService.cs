using Microsoft.AspNetCore.Identity;
using OnlineEdu.WebUI.DTOs.UserDTOs;
using OnlineEdu.WebUI.Models;

namespace OnlineEdu.WebUI.Services.UserServices
{
	public interface IUserService
	{
		Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto);
		Task<string> LoginAsync(UserLoginDto userLoginDto);
		Task LogoutAsync();
		Task<bool> CreateRoleAsync(UserRoleDto userRoleDto);
		Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto);
		Task<List<AssignRoleDto>> GetUserForRoleAssign(int id);
		Task<List<UserViewModel>> GetAllUsersAsync();
		Task<List<ResultUserDto>> GetFourTeachers();
		Task<int> GetTeacherCount();
		Task<List<ResultUserDto>> GetAllTeachers();
	}
}
