namespace ToDoApp.Models.Dtos.Users.Requests;

public sealed record ChangePasswordRequestDto(string CurrentPassword, string NewPassword, string ConfirmPassword);