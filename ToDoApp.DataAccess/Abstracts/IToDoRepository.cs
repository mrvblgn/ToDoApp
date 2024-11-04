using Core.Repositories;
using ToDoApp.Models.Entities;

namespace ToDoApp.DataAccess.Abstracts;

public interface IToDoRepository : IRepository<ToDo, Guid>
{
    
}