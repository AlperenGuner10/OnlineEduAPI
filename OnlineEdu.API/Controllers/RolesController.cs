using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DTO.DTOs.RoleDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles ="Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;

		public RolesController(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_roleManager=roleManager;
			_mapper=mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var values = await _roleManager.Roles.ToListAsync();
			return Ok(values);
		}
		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
		{
			var role = _mapper.Map<AppRole>(createRoleDto);
			var result = await _roleManager.CreateAsync(role);
			if(!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}
			return Ok("Rol Oluşturuldu");
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var role = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id == id);
			if(role == null)
			{
				return NotFound("Rol Bulunamadı");
			}
			await _roleManager.DeleteAsync(role);
			return Ok("Rol Silindi");
		}
	}
}
