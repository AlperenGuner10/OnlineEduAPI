using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.UserDTOs;
using OnlineEdu.Entity.Entities;
using System.Security.Claims;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleAssignsController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		public RoleAssignsController(IUserService userService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_userService=userService;
			_userManager=userManager;
			_roleManager=roleManager;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllUsersAsync()
		{
			var values = await _userService.GetAllUserAsync();

			var userList = (from x in values
							select new UserListDto
							{
								Id = x.Id,
								NameSurname = x.FirstName+ " "+x.LastName,
								UserName = x.UserName,
								Roles = _userManager.GetRolesAsync(x).Result.ToList()
							}).ToList();
			return Ok(userList);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserForRoleAssign(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);

			var roles = await _roleManager.Roles.ToListAsync();

			var userRoles = await _userManager.GetRolesAsync(user);

			List<AssignRoleDto> assignRoleList = new List<AssignRoleDto>();

			foreach (var role in roles)
			{
				var assignRole = new AssignRoleDto();
				assignRole.UserId  = user.Id;
				assignRole.RoleId = role.Id;
				assignRole.RoleName = role.Name;
				assignRole.RoleExist = userRoles.Contains(role.Name);

				assignRoleList.Add(assignRole);
			}
			return Ok(assignRoleList);
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(List<AssignRoleDto> assignRoleList)
		{
			int userId = assignRoleList.Select(x=>x.UserId).FirstOrDefault();

			var user = await _userService.GetUserByIdAsync(userId);

			foreach (var item in assignRoleList)
			{
				if (item.RoleExist)
				{
					await _userManager.AddToRoleAsync(user, item.RoleName);
				}
				else
				{
					await _userManager.RemoveFromRoleAsync(user, item.RoleName);
				}
			}
			return Ok("Rol Atama Başarılı");
		}
	}
}
