

namespace OnlineEdu.WebUI.DTOs.CourseCategoryDTOs
{
	public class UpdateCourseCategoryDto
	{
		public int CourseCategoryId { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Description { get; set; }
		public bool IsShown { get; set; }
	}
}
