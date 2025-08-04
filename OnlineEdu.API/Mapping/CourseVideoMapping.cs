using AutoMapper;
using OnlineEdu.DTO.DTOs.CourseVideoDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class CourseVideoMapping : Profile
	{
		public CourseVideoMapping()
		{
			CreateMap<CreateCourseVideoDto, CourseVideo>().ReverseMap();
			CreateMap<UpdateCourseVideoDto, CourseVideo>().ReverseMap();
			CreateMap<ResultCourseVideoDto, CourseVideo>().ReverseMap();
		}
	}
}
