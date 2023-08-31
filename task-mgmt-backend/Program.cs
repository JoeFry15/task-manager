using Microsoft.EntityFrameworkCore;
using task_mgmt_backend.Models;
using task_mgmt_backend.Repositories;
using task_mgmt_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string allowAllCorsPolicy = "_allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<TaskContext>(opt =>
    opt.UseInMemoryDatabase("TaskList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITaskManagerService, TaskManagerService>();
builder.Services.AddTransient<ITaskManagerRepo, TaskManagerRepo>();

builder.Services.AddTransient<TaskContext>();

var app = builder.Build();


// Check context is speaking to the database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<TaskContext>();

// Initialise TaskItems with sample data
context.TaskItems.RemoveRange(context.TaskItems);

context.TaskItems.Add(new TaskItem()
    {
    Id = 1, 
    Name ="Walk",
    IsComplete = false,
    });

context.TaskItems.Add(new TaskItem()
    {
    Id = 2, 
    Name ="Shop",
    IsComplete = true,
    });

context.SaveChanges();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowAllCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
