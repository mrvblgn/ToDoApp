using Core.Exceptions;
using ToDoApp.Models.Entities;

namespace ToDoApp.Service.Rules;

public class ToDoBusinessRules
{
    public virtual void ToDoIsNullCheck(ToDo toDo)
    {
        if(toDo is null)
        {
            throw new NotFoundException("Yapılacak iş bulunamadı.");
        }
    }
}