using AutoMapper;
using OnlineEdu.DTO.DTOs.BlogCategoryDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class BlogCategoryMapping : Profile
	{
		public BlogCategoryMapping()
		{
			CreateMap<CreateBlogCategoryDto, BlogCategory >();
			CreateMap<UpdateBlogCategoryDto, BlogCategory >();
			CreateMap<ResultBlogCategoryDto, BlogCategory >();
		}
	}
}
