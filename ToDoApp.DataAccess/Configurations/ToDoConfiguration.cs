using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Models.Entities;

namespace ToDoApp.DataAccess.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ToDoId");
        builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(x => x.Title).HasColumnName("Title");
        builder.Property(x => x.Description).HasColumnName("Description");
        builder.Property(x => x.StartDate).HasColumnName("StartDate");
        builder.Property(x => x.EndDate).HasColumnName("EndDate");
        builder.Property(x => x.Priority).HasColumnName("Priority");
        builder.Property(x => x.Completed).HasColumnName("Completed");
        builder.Property(x => x.CategoryId).HasColumnName("CategoryId");
        builder.Property(x => x.UserId).HasColumnName("UserId");
        
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Todos)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.ToDos)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Navigation(x => x.User).AutoInclude();
        builder.Navigation(x => x.Category).AutoInclude();
    }
}