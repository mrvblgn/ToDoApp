using FluentValidation;
using ToDoApp.Models.Dtos.Categories.Requests;

namespace ToDoApp.Service.Validations.Categories;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz!");
    }
}