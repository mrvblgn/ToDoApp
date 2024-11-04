using Core.Repositories;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.DataAccess.Contexts;
using ToDoApp.Models.Entities;

namespace ToDoApp.DataAccess.Concretes;

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
{
    public EfCategoryRepository(BaseDbContext context) : base(context)
    {
        
    }
}