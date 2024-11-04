using Core.Entities;

namespace ToDoApp.Models.Entities;

public class ToDo : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public bool Completed { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

    public ToDo()
    {
        Completed = false;
    }
}