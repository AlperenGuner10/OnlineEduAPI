using OnlineEdu.DTO.DTOs.BlogDTOs;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DTO.DTOs.BlogCategoryDTOs
{
	public class UpdateBlogCategoryDto
	{
		public int BlogCategoryId { get; set; }
		public string Name { get; set; }
	}
}
