using Microsoft.AspNetCore.Mvc;
using task_mgmt_backend.Models;
using task_mgmt_backend.Services;

namespace task_mgmt_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskManagerController : ControllerBase
{
    private readonly ILogger<TaskManagerController> _logger;
    private readonly ITaskManagerService _taskManagerService;

    public TaskManagerController(ITaskManagerService taskManagerService, ILogger<TaskManagerController> logger)
    {
        _logger = logger;
        _taskManagerService = taskManagerService;
    }

    [HttpGet("all")]
    public IActionResult GetAllTasks()
    {
        try
        {
            var tasks = _taskManagerService.GetAllTasks();
            return Ok(tasks);
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById([FromRoute] int Id)
    {
        try
        {
            var task = _taskManagerService.GetById(Id);
            return Ok(task);
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }
}
