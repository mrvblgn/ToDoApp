using FluentValidation;
using ToDoApp.Models.Dtos.ToDos.Requests;

namespace ToDoApp.Service.Validations.ToDos;

public class CreateToDoRequestValidator : AbstractValidator<CreateToDoRequest>
{
    public CreateToDoRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!")
            .MinimumLength(2).WithMessage("Post başlığı minimum iki karakterli olmalıdır");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
            .MinimumLength(2).WithMessage("Açıklama alanı minimum iki karakterli olmalı!");

        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlama tarihi boş olamaz!");

        RuleFor(x => x.EndDate).NotEmpty().WithMessage("Bitiş tarihi boş olamaz!");

        RuleFor(x => x.Priority).NotEmpty().WithMessage("Öncelik boş bırakılamaz");
    }
}