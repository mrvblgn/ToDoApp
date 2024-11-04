using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Dtos.ToDos.Requests;

public sealed record FilterToDoRequest
    (
        DateTime? StartDate, 
        DateTime? EndDate, 
        string Title, 
        string? Description,
        Priority? Priority,
        bool? Completed
    );