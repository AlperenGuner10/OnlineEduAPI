using AutoMapper;
using OnlineEdu.DTO.DTOs.SubscriberDTOs;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Mapping
{
	public class SubscribeMapping : Profile
	{
		public SubscribeMapping()
		{
			CreateMap<CreateSubscriberDto, Subscriber>().ReverseMap();
			CreateMap<UpdateSubscriberDto, Subscriber>().ReverseMap();
		}
	}
}
