
namespace EasyTodoListApp.Domain;

public record Identifier<T>(Guid Value)
{
   public static Identifier<T> Create(Guid Id) => new(Id);
}
