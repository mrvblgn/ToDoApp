using Core.Exceptions;
using ToDoApp.Models.Entities;

namespace ToDoApp.Service.Rules;

public class CategoryBusinessRules
{
    public virtual void CategoryIsNullCheck(Category toDo)
    {
        if(toDo is null)
        {
            throw new NotFoundException("Kategori bulunamadı.");
        }
    }
}