﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.DTOs.TestimonialDTOs
{
	public class UpdateTestimonialDto
	{
		public int TestimonialId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public string Comment { get; set; }
		public int Star { get; set; }
	}
}
