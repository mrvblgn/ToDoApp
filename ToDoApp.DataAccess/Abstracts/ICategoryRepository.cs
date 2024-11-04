using Core.Repositories;
using ToDoApp.Models.Entities;

namespace ToDoApp.DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category, int>
{
    
}