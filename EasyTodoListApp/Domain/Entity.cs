
namespace EasyTodoListApp.Domain;

public abstract class Entity<T> 
{
   public abstract Identifier<T> Identifier { get; init; }
}
