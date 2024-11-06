using FluentValidation;
using ToDoApp.Models.Dtos.ToDos.Requests;

namespace ToDoApp.Service.Validations.ToDos;

public class FilterToDoRequestValidator : AbstractValidator<FilterToDoRequest>
{
    public FilterToDoRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("Başlama tarihi, bitiş tarihinden önce olmalıdır.");

        RuleFor(x => x.Title)
            .MinimumLength(2).WithMessage("Başlık alanı minimum iki karakterli olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Description)
            .MinimumLength(2).WithMessage("Açıklama alanı minimum iki karakterli olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Geçersiz öncelik değeri.");
    }
}