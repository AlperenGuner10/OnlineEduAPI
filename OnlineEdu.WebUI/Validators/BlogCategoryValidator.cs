using FluentValidation;
using OnlineEdu.WebUI.DTOs.BlogCategoryDTOs;

namespace OnlineEdu.WebUI.Validators
{
	public class BlogCategoryValidator : AbstractValidator<CreateBlogCategoryDto>
	{
		public BlogCategoryValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Blog Kategori Adı Boş Bırakılamaz");
			RuleFor(x => x.Name).MaximumLength(30).WithMessage("Blog Kategori Alanı En Fazla 30 Karakterli Olmalıdır");
		}
	}
}
