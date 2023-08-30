using Microsoft.EntityFrameworkCore;

namespace task_mgmt_backend.Models;

public class TaskContext : DbContext
{

    protected readonly IConfiguration Configuration;

    public TaskContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<TaskItem> TaskItems { get; set; } = null!;
}