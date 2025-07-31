using AutoMapper;
using OnlineEdu.DTO.DTOs.CourseRegisterDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class CourseRegisterMapping : Profile
	{
		public CourseRegisterMapping()
		{
			CreateMap<ResultCourseRegisterDto, CourseRegister>().ReverseMap();
			CreateMap<UpdateCourseRegisterDto, CourseRegister>().ReverseMap();
			CreateMap<CreateCourseRegisterDto, CourseRegister>().ReverseMap();
		}
	}
}
