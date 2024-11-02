namespace ToDoApp.Models.Dtos.Users.Requests;

public sealed record RegisterRequestDto
    (
        string FirstName,
        string LastName,
        string Email,
        string Username,
        string Password,
        string City
    );