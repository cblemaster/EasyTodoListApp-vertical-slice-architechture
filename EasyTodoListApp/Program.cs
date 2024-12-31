
using EasyTodoListApp.Infrastructure.DatabaseContext;
using EasyTodoListApp.Infrastructure.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddDbContext<EasyTodoListAppDbContext>()
    .AddScoped<ITodoRepository, TodoRepository>()
    .AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddEndpointsApiExplorer();
WebApplication app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
