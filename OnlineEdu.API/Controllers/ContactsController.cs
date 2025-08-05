using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Business.Abstract;
using OnlineEdu.DTO.DTOs.ContactDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController(IGenericService<Contact> _contactService, IMapper _mapper) : ControllerBase
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var values = _contactService.TGetList();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var value = _contactService.TGetById(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_contactService.TDelete(id);
			return Ok("İletişim Alanı Silindi");
		}
		[HttpPost]
		public IActionResult Create(CreateContactDto createContactDTO)
		{
			var value = _mapper.Map<Contact>(createContactDTO);
			_contactService.TCreate(value);
			return Ok("Yeni İletişim Alanı Oluşturuldu.");
		}
		[HttpPut]
		public IActionResult Update(UpdateContactDto updateContactDTO)
		{
			var value = _mapper.Map<Contact>(updateContactDTO);
			_contactService.TUpdate(value);
			return Ok("İletişim Alanı Güncellendi");
		}
	}
}
