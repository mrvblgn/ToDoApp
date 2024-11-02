using Core.Entities;

namespace ToDoApp.Models.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; }
    public List<ToDo> ToDos { get; set; }
}