using OnlineEdu.WebUI.DTOs.CourseDTOs;

namespace OnlineEdu.WebUI.DTOs.CourseVideoDTOs
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
