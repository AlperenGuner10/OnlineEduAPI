using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.BlogDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles = "Admin, Teacher")]
	[Route("api/[controller]")]
	[ApiController]
	public class BlogsController(IMapper _mapper, IBlogService _blogService) : ControllerBase
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var values = _blogService.TGetBlogsWithCategories();
			var blogs = _mapper.Map<List<ResultBlogDto>>(values);
			return Ok(blogs);
		}
		[AllowAnonymous]
		[HttpGet("GetLastFourBlogs")]
		public IActionResult GetLastFourBlogs()
		{
			var values = _blogService.TGetLastFourBlogsWithCategories();
			var blogs = _mapper.Map<List<ResultBlogDto>>(values);
			return Ok(blogs);
		}
		[AllowAnonymous]
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _blogService.TGetBlogWithCategory(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_blogService.TDelete(id);
			return Ok("Blog Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateBlogDto createBlogDTO)
		{
			var value = _mapper.Map<Blog>(createBlogDTO);
			_blogService.TCreate(value);
			return Ok("Yeni Blog Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateBlogDto updateBlogDTO)
		{
			var value = _mapper.Map<Blog>(updateBlogDTO);
			_blogService.TUpdate(value);
			return Ok("Blog Alanı Güncellendi");
		}
		[HttpGet("GetBlogByWriterId/{id}")]
		public IActionResult GetBlogByWriterId(int id)
		{
			var values = _blogService.TGetBlogsWithCategoriesByWriterId(id);
			var mappedValues = _mapper.Map<List<ResultBlogDto>>(values);
			return Ok(mappedValues);
		}
		[AllowAnonymous]
		[HttpGet("GetBlogCount")]
		public IActionResult GetBlogCount()
		{
			var blogCount = _blogService.TCount();
			return Ok(blogCount);
		}
		[AllowAnonymous]
		[HttpGet("GetBlogsByCategoryId/{id}")]
		public IActionResult GetBlogsByCategoryId(int id)
		{
			var blogs = _blogService.TGetBlogsCategoriesByCategoryId(id);
			return Ok(blogs);
		}

	}
}
