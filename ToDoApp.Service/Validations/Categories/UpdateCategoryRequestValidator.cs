using FluentValidation;
using ToDoApp.Models.Dtos.Categories.Requests;

namespace ToDoApp.Service.Validations.Categories;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id alanı boş olamaz");
        RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz!");
    }
}