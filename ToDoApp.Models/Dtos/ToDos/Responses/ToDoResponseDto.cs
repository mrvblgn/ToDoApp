using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Dtos.ToDos.Responses;

public sealed record ToDoResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime CreatedDate { get; init; }
    public Priority Priority { get; init; }
    public bool Completed { get; init; }
    public string Category { get; init; }
    public string UserName { get; init; }
}