namespace ToDoApp.Models.Dtos.Users.Responses;

public sealed record UserResponseDto
{
    public long Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
}