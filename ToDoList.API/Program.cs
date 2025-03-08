using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoList.Application.Services;
using ToDoList.Domain;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// خواندن ConnectionString از appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ثبت DbContext برای EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ثبت Repository برای Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToDoList API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
