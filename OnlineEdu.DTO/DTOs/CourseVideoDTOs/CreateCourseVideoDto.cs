using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DTO.DTOs.CourseVideoDTOs
{
	public class CreateCourseVideoDto
	{
		public int CourseId { get; set; }
		public int VideoNumber { get; set; }
		public string VideoURL { get; set; }
	}
}
