using OnlineEdu.WebUI.DTOs.BlogDTOs;
using OnlineEdu.WebUI.DTOs.CourseDTOs;
using OnlineEdu.WebUI.DTOs.CourseRegisterDTOs;
using OnlineEdu.WebUI.DTOs.TeacherSocialDTOs;

namespace OnlineEdu.WebUI.DTOs.UserDTOs
{
	public class ResultUserDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string? ImageUrl { get; set; }
		public List<ResultTeacherSocialDto> TeacherSocials { get; set; }
		public List<ResultCourseDto> Course { get; set; }
		public List<ResultCourseRegisterDto> CourseRegister { get; set; }
		public List<ResultBlogDto> Blogs { get; set; }
	}
}
