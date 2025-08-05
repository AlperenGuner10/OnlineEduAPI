using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DataAccess.Context;
using OnlineEdu.DTO.DTOs.UserDTOs;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Business.Concrete
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly OnlineEduContext _context;
		private readonly IMapper _mapper;

		public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, OnlineEduContext context)
		{
			_userManager=userManager;
			_signInManager=signInManager;
			_roleManager=roleManager;
			_mapper=mapper;
			_context=context;
		}

		public Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto)
		{
			throw new NotImplementedException();
		}

		public Task<bool> CreateRoleAsync(UserRoleDto userRoleDto)
		{
			throw new NotImplementedException();
		}

		public async Task<IdentityResult> CreateUserAsync(RegisterDto userRegisterDto)
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
			var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Student");
				return result;
			}
			return result;
		}

		public async Task<List<ResultUserDto>> GetFourTeachers()
		{
			var users = await _userManager.Users.Include(x => x.TeacherSocials).ToListAsync();

			var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).OrderByDescending(x => x.Id).Take(4).ToList();

			return _mapper.Map<List<ResultUserDto>>(teachers);
		}

		public async Task<List<AppUser>> GetAllUserAsync()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<AppUser> GetUserByIdAsync(int id)
		{
			return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<string> LoginAsync(LoginDto userLoginDto)
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

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<int> GetTeacherCount()
		{
			var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
			return teachers.Count();
		}

		public async Task<List<ResultUserDto>> GetAllTeachers()
		{
			var users = await _userManager.Users.Include(x => x.TeacherSocials).ToListAsync();

			var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).ToList();

			return _mapper.Map<List<ResultUserDto>>(teachers);
		}
	}
}
