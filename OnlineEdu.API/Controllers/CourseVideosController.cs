using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.CourseVideoDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles = "Admin, Teacher, Student")]
	[Route("api/[controller]")]
	[ApiController]
	public class CourseVideosController : ControllerBase
	{
		private readonly IGenericService<CourseVideo> _courseVideoService;
		private readonly IMapper _mapper;

		public CourseVideosController(IGenericService<CourseVideo> courseVideoService, IMapper mapper)
		{
			_courseVideoService=courseVideoService;
			_mapper=mapper;
		}
		[HttpGet]
		public IActionResult Get()
		{
			var values = _courseVideoService.TGetList();
			return Ok(values);
		}
		[HttpGet("GetCourseVideosByCourseId/{id}")]
		public IActionResult GetCourseVideosByCourseId(int id)
		{
			var values = _courseVideoService.TGetFilteredList(x => x.CourseId==id);
			return Ok(values);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _courseVideoService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_courseVideoService.TDelete(id);
			return Ok("Kurs Video Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateCourseVideoDto createCourseVideoDto)
		{
			var value = _mapper.Map<CourseVideo>(createCourseVideoDto);
			_courseVideoService.TCreate(value);
			return Ok("Yeni Kurs Video Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateCourseVideoDto updateCourseVideoDto)
		{
			var value = _mapper.Map<CourseVideo>(updateCourseVideoDto);
			_courseVideoService.TUpdate(value);
			return Ok("Kurs Video Alanı Güncellendi");
		}
	}
}
