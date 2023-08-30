using task_mgmt_backend.Models;
using Microsoft.EntityFrameworkCore;



namespace task_mgmt_backend.Repositories;

public interface ITaskManagerRepo
{
    public TaskItem GetById(int id);
}

public class TaskManagerRepo : ITaskManagerRepo
{
    private readonly TaskContext _context;
    public TaskManagerRepo(TaskContext context)
    {
        _context = context;
    }

    public TaskItem GetById(int id)
    {
        try
        {
            return _context.TaskItems
                .FirstOrDefault(item => item.Id == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No task with id {id} found in the database", ex);
        }
    }
}