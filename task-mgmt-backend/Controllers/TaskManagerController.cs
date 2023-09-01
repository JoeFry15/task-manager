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

    [HttpPost("create")]
    public IActionResult CreateTask([FromBody] string task)
    {
        try
        {
            _taskManagerService.CreateTask(task);
            return Ok($"Your task has been created!");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("complete/{id}")]
    public ActionResult CompleteTask([FromRoute] int id)
    {
        _taskManagerService.CompleteTask(id);
        return Ok("Completed");
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
