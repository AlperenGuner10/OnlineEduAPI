﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.AboutDTOs;
using OnlineEdu.DTO.DTOs.MessageDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessagesController(IGenericService<Message> _messageService, IMapper _mapper) : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var values = _messageService.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _messageService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_messageService.TDelete(id);
			return Ok("Mesaj Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateMessageDto createMessageDto)
		{
			var value = _mapper.Map<Message>(createMessageDto);
			_messageService.TCreate(value);
			return Ok("Yeni Mesaj Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateMessageDto updateMessageDto)
		{
			var value = _mapper.Map<Message>(updateMessageDto);
			_messageService.TUpdate(value);
			return Ok("Mesaj Alanı Güncellendi");
		}
	}
}
