using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.BannerDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class BannersController(IGenericService<Banner> _bannerService, IMapper _mapper) : ControllerBase
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var values = _bannerService.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _bannerService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_bannerService.TDelete(id);
			return Ok("Banner Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateBannerDTO createBannerDTO)
		{
			var value = _mapper.Map<Banner>(createBannerDTO);
			_bannerService.TCreate(value);
			return Ok("Yeni Banner Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateBannerDTO updateBannerDTO)
		{
			var value = _mapper.Map<Banner>(updateBannerDTO);
			_bannerService.TUpdate(value);
			return Ok("Banner Alanı Güncellendi");
		}
	}
}
