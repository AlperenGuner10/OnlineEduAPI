﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.DTOs.CourseDTOs
{
	public class UpdateCourseDto
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string ImageUrl { get; set; }
		public int CategoryId { get; set; }
		public decimal Price { get; set; }
		public bool IsShown { get; set; }
	}
}
