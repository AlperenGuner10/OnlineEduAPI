
namespace OnlineEdu.WebUI.DTOs.CourseVideoDTOs
{
	public class UpdateCourseVideoDto
	{
		public int CourseVideoId { get; set; }
		public int CourseId { get; set; }
		public int VideoNumber { get; set; }
		public string VideoURL { get; set; }
	}
}
