using OnlineEdu.DTO.DTOs.CourseDTOs;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DTO.DTOs.CourseVideoDTOs
{
	public class ResultCourseVideoDto
	{
		public int CourseVideoId { get; set; }
		public int CourseId { get; set; }
		public ResultCourseDto Course { get; set; }
		public int VideoNumber { get; set; }
		public string VideoURL { get; set; }
	}
}
