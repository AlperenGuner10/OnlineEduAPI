using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.RoleDTOs;

namespace OnlineEdu.WebUI.Services.RoleServices
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;

		public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_roleManager=roleManager;
			_mapper=mapper;
		}

		public async Task CreateRoleAsync(CreateRoleDto createRoleDto)
		{
			var value = _mapper.Map<AppRole>(createRoleDto);
			await _roleManager.CreateAsync(value);
		}

		public async Task DeleteRoleAsync(int id)
		{
			var value = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id == id);
			await _roleManager.DeleteAsync(value);
		}

		public async Task<List<ResultRoleDto>> GetAllRolesAsync()
		{
			var values = await _roleManager.Roles.ToListAsync();
			return _mapper.Map<List<ResultRoleDto>>(values);
		}

		public async Task<UpdateRoleDto> GetRoleByIdAsync(int id)
		{
			var value = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
			return _mapper.Map<UpdateRoleDto>(value);
		}
	}
}
