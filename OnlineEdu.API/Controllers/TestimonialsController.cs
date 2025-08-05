using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.AboutDTOs;
using OnlineEdu.DTO.DTOs.TestimonialDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialsController(IGenericService<Testimonial> _testimonialController, IMapper _mapper) : ControllerBase
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var values = _testimonialController.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _testimonialController.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_testimonialController.TDelete(id);
			return Ok("Referans Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateTestimonialDto createTestimonialDto)
		{
			var value = _mapper.Map<Testimonial>(createTestimonialDto);
			_testimonialController.TCreate(value);
			return Ok("Yeni Referans Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateTestimonialDto updateTestimonialDto)
		{
			var value = _mapper.Map<Testimonial>(updateTestimonialDto);
			_testimonialController.TUpdate(value);
			return Ok("Referans Alanı Güncellendi");
		}
		[AllowAnonymous]
		[HttpGet("GetTestimonialCount")]
		public IActionResult GetTestimonialCount()
		{
			var courseCount = _testimonialController.TCount();
			return Ok(courseCount);
		}
	}
}
