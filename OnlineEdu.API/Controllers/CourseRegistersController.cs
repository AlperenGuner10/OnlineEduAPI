using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.CourseRegisterDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseRegistersController : ControllerBase
	{
		private readonly ICourseRegisterService _courseRegisterService;
		private readonly IMapper _mapper;

		public CourseRegistersController(ICourseRegisterService courseRegisterService, IMapper mapper)
		{
			_courseRegisterService=courseRegisterService;
			_mapper=mapper;
		}

		[HttpGet("GetMyCourses/{userId}")]
		public IActionResult GetMyCourses(int userId)
		{
			var values = _courseRegisterService.TGetAllWithCourseAndCategory(x=>x.AppUserId == userId);
			var mappedValue = _mapper.Map<List<ResultCourseRegisterDto>>(values);
			return Ok(mappedValue);
		}

		[HttpPost]
		public IActionResult RegisterToCourse(CreateCourseRegisterDto createCourseRegisterDto)
		{
			var newCourseRegister = _mapper.Map<CourseRegister>(createCourseRegisterDto);
			_courseRegisterService.TCreate(newCourseRegister);
			return Ok("Kurs Kaydı Başarılı");
		}

		[HttpPut]
		public IActionResult UpdateCourseRegister(UpdateCourseRegisterDto updateCourseRegisterDto)
		{
			var updateCourseRegister = _mapper.Map<CourseRegister>(updateCourseRegisterDto);
			_courseRegisterService.TUpdate(updateCourseRegister);
			return Ok("Kurs Kaydı Güncellendi");
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _courseRegisterService.TGetById(id);
			var mappedValue = _mapper.Map<ResultCourseRegisterDto>(value);
			return Ok(mappedValue);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteCourseRegister(int id)
		{
			_courseRegisterService.TDelete(id);
			return Ok("Kurs Kaydı Silindi");
		}
	}
}
