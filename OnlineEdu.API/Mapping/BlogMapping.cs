using AutoMapper;
using OnlineEdu.DTO.DTOs.BlogDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class BlogMapping : Profile
	{
		public BlogMapping()
		{
			CreateMap<CreateBlogDto, Blog>();
			CreateMap<UpdateBlogDto, Blog>();
			CreateMap<ResultBlogDto, Blog>();
		}
	}
}
