using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Dtos.ToDos.Requests;

public sealed record UpdateToDoRequest
    (
        Guid Id, 
        string Title, 
        string Description, 
        DateTime StartDate, 
        DateTime EndDate,
        Priority Priority,
        bool Completed
    );