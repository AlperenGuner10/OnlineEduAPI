﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.AboutDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutsController(IGenericService<About> _aboutService, IMapper _mapper) : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var values = _aboutService.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _aboutService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_aboutService.TDelete(id);
			return Ok("Hakkımızda Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateAboutDTO createAboutDTO)
		{
			var value = _mapper.Map<About>(createAboutDTO);
			_aboutService.TCreate(value);
			return Ok("Yeni Hakkımızda Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateAboutDTO updateAboutDTO)
		{
			var value = _mapper.Map<About>(updateAboutDTO);
			_aboutService.TUpdate(value);
			return Ok("Hakkımızda Alanı Güncellendi");
		}
	}
}
