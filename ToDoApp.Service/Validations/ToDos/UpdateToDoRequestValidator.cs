using FluentValidation;
using ToDoApp.Models.Dtos.ToDos.Requests;

namespace ToDoApp.Service.Validations.ToDos;

public class UpdateToDoRequestValidator : AbstractValidator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID alanı boş olamaz!");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık alanı boş olamaz!")
            .MinimumLength(2).WithMessage("Başlık alanı minimum iki karakterli olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama alanı boş olamaz!")
            .MinimumLength(2).WithMessage("Açıklama alanı minimum iki karakterli olmalıdır.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Başlama tarihi boş olamaz!");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Bitiş tarihi boş olamaz!")
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("Bitiş tarihi, başlama tarihinden önce olamaz.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Geçersiz öncelik değeri.");

        RuleFor(x => x.Completed)
            .NotNull().WithMessage("Tamamlanma durumu boş olamaz.");
    }
}