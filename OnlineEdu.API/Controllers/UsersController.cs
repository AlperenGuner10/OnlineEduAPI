using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.UserDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IJwtService _jwtService;
		private readonly IMapper _mapper;
		public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService, IMapper mapper)
		{
			_userManager=userManager;
			_signInManager=signInManager;
			_jwtService=jwtService;
			_mapper=mapper;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				return BadRequest("Bu E-Mail Sistemde Kayıtlı Değil");
			}

			var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

			if (!result.Succeeded)
			{
				return BadRequest("Kullanıcı Adı veya Şifre Hatalı");
			}

			var token = await _jwtService.CreateTokenAsync(user);
			return Ok(token);
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDto registerDto)
		{
			var user = _mapper.Map<AppUser>(registerDto);

			if (ModelState.IsValid)
			{
				var result = await _userManager.CreateAsync(user, registerDto.Password);
				if (!result.Succeeded)
				{
					return BadRequest(result.Errors);
				}
				await _userManager.AddToRoleAsync(user, "Student");
				return Ok("Kullanıcı Kayıt Edildi");
			}

			return BadRequest(ModelState);
		}
		[HttpGet("TeacherList")]
		public async Task<IActionResult> TeacherList()
		{
			var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
			return Ok(teachers);
		}
		[HttpGet("StudentList")]
		public async Task<IActionResult> StudentList()
		{
			var students = await _userManager.GetUsersInRoleAsync("Student");
			return Ok(students);
		}

		[HttpGet("GetFourTeachers")]
		public async Task<IActionResult> GetFourTeachers()
		{
			var users = await _userManager.Users.Include(x => x.TeacherSocials).ToListAsync();

			var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).OrderByDescending(x => x.Id).Take(4).ToList();

			return Ok(teachers);
		}
	}
}
