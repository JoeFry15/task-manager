using task_mgmt_backend.Models;
using Microsoft.EntityFrameworkCore;



namespace task_mgmt_backend.Repositories;

public interface ITaskManagerRepo
{
    public List<TaskItem> GetAllTasks();
    public void CreateTask(string s);
    public void CompleteTask(int id);
    public TaskItem GetById(int id);
}

public class TaskManagerRepo : ITaskManagerRepo
{
    private readonly TaskContext _context;
    public TaskManagerRepo(TaskContext context)
    {
        _context = context;
    }

    public List<TaskItem> GetAllTasks()
    {
        try
        {
            return _context.TaskItems.ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No tasks found in the database", ex);
        }
    }

    async public void CreateTask(string s)
    {
        _context.TaskItems.Add(new TaskItem()
            {
            Name = s,
            IsComplete = false,
            });

        _context.SaveChanges();
    }

    public void CompleteTask(int id)
    {
        try
        {
            var selectedTask = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            selectedTask.IsComplete = true;
            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No task with id {id} found in the database", ex);
        }
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