
namespace EasyTodoListApp.Domain;

public class Todo : Entity<Todo>
{
   public Descriptor Description { get; init; } = default!;
   public DateOnly? DueDate { get; init; }
   public bool IsImportant { get; init; }
   public bool IsComplete { get; init; }
   public DateTimeStamps Dates { get; init; } = default!;
   public override Identifier<Todo> Identifier { get; init; } = default!;

   private Todo() { }

   private Todo(string description, DateOnly? dueDate, bool isImportant, bool isComplete)
   {
      Description = new(description);
      DueDate = dueDate;
      IsImportant = isImportant;
      IsComplete = isComplete;
      Dates = new(DateTime.Now, null);
   }

   public static Todo Create(string description, DateOnly? dueDate, bool isImportant, bool isComplete) =>new(description, dueDate, isImportant, isComplete);
}
