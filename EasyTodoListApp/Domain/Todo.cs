
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
      Description = Descriptor.CreateOrThrowArgException(description, IS_DSCRIPTION_REQUIRED, IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED, MAX_LENGTH_FOR_DESCRIPTION);
      DueDate = dueDate;
      IsImportant = isImportant;
      IsComplete = isComplete;
      Dates = new(DateTime.Now, null);
      Identifier = Identifier<Todo>.Create(Guid.NewGuid());
   }

   public static Todo Create(string description, DateOnly? dueDate, bool isImportant, bool isComplete) =>new(description, dueDate, isImportant, isComplete);

   public const int MAX_LENGTH_FOR_DESCRIPTION = 100;
   public const bool IS_DSCRIPTION_REQUIRED = true;
   public const bool IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED = false;
}
