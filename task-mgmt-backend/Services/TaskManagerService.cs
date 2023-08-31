using task_mgmt_backend.Models;
using task_mgmt_backend.Repositories;

namespace task_mgmt_backend.Services;

public interface ITaskManagerService
{
    public List<TaskItem> GetAllTasks();
    public TaskItem GetById(int id);
}

public class TaskManagerService : ITaskManagerService
{
    private readonly ITaskManagerRepo _taskManager;


    public TaskManagerService(ITaskManagerRepo taskManager)
    {
        _taskManager = taskManager;

    }

    public List<TaskItem> GetAllTasks()
    {
        return _taskManager.GetAllTasks();
    }

    public TaskItem GetById(int id)
    {
        return _taskManager.GetById(id);
    }
}