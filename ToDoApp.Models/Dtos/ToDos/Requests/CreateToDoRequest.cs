using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Dtos.ToDos.Requests;

public sealed record CreateToDoRequest
    (
        string Title, 
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        Priority Priority,
        int CategoryId,
        string UserId
    );