using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.UserDTOs;

namespace OnlineEdu.WebUI.Services.UserServices
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;

		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
		{
			_userManager=userManager;
			_signInManager=signInManager;
			_roleManager=roleManager;
		}

		public Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto)
		{
			throw new NotImplementedException();
		}

		public Task<bool> CreateRoleAsync(UserRoleDto userRoleDto)
		{
			throw new NotImplementedException();
		}

		public async Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto)
		{
			var user = new AppUser()
			{
				FirstName = userRegisterDto.FirstName,
				LastName = userRegisterDto.LastName,
				UserName = userRegisterDto.UserName,
				Email = userRegisterDto.Email
			};
			if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
			{
				return new IdentityResult();
			}
			return await _userManager.CreateAsync(user, userRegisterDto.Password);

		}

		public async Task<List<AppUser>> GetAllUserAsync()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<AppUser> GetUserByIdAsync(int id)
		{
			return await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
		}

		public async Task<string> LoginAsync(UserLoginDto userLoginDto)
		{
			var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
			if (user == null)
			{
				return null;
			}

			var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);

			if (!result.Succeeded)
			{
				return null;
			}

			else
			{
				var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
				if (isAdmin)
				{
					return "Admin";
				}

				var isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
				if (isTeacher)
				{
					return "Teacher";
				}
				var isStudent = await _userManager.IsInRoleAsync(user, "Student");
				if (isStudent)
				{
					return "Student";
				}
			}
			return null;

		}

		public Task<bool> LogoutAsync()
		{
			throw new NotImplementedException();
		}
	}
}
