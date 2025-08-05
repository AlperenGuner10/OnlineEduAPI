using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.DTOs.UserDTOs;

namespace OnlineEdu.WebUI.DTOs.CourseRegisterDTOs
{
	public class ResultCourseRegisterDto
	{
		public int CourseRegisterId { get; set; }
		public int AppUserId { get; set; }
		public ResultUserDto AppUser { get; set; }
		public int CourseId { get; set; }
		public ResultCourseDto Course { get; set; }
	}
}
