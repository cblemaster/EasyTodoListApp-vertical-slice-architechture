
namespace EasyTodoListApp.Domain;

public record DateTimeStamps(DateTime CreateDate, DateTime? UpdateDate)
{
    public static DateTimeStamps Create(DateTime createDate, DateTime? updateDate) => new(createDate, updateDate);
}
