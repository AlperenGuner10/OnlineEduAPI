using AutoMapper;
using OnlineEdu.DTO.DTOs.TeacherSocialsDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class TeacherSocialMapping : Profile
	{
		public TeacherSocialMapping()
		{
			CreateMap<ResultTeacherSocialDto, TeacherSocial>().ReverseMap();
			CreateMap<UpdateTeacherSocialDto, TeacherSocial>().ReverseMap();
			CreateMap<CreateTeacherSocialDto, TeacherSocial>().ReverseMap();
		}
	}
}
