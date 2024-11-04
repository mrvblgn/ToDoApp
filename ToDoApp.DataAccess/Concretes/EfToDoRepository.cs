using Core.Repositories;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.DataAccess.Contexts;
using ToDoApp.Models.Entities;

namespace ToDoApp.DataAccess.Concretes;

public class EfToDoRepository : EfRepositoryBase<BaseDbContext, ToDo, Guid>, IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {
        
    }
}