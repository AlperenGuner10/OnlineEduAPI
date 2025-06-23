using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.BlogCategoryDTOs;
using OnlineEdu.DTO.DTOs.BlogDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogCategoriesController(IGenericService<BlogCategory> blogCategoryService, IMapper _mapper) : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var values = blogCategoryService.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = blogCategoryService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			blogCategoryService.TDelete(id);
			return Ok("Blog Kategorisi Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateBlogCategoryDto createBlogCategoryDTO)
		{
			var value = _mapper.Map<BlogCategory>(createBlogCategoryDTO);
			blogCategoryService.TCreate(value);
			return Ok("Yeni Blog Kategorisi Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateBlogCategoryDto updateBlogCategoryDTO)
		{
			var value = _mapper.Map<BlogCategory>(updateBlogCategoryDTO);
			blogCategoryService.TUpdate(value);
			return Ok("Blog Kategorisi Alanı Güncellendi");
		}
	}
}
