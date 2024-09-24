using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodosAPI.Application.UseCases.CreateTask;
using TodosAPI.Core.Entities;
using TodosAPI.Core.Services;
using TodosAPI.Infrastructure.Dto_s;
using TodosAPI.Infrastructure.Repositories;

[ApiController]
[Route("api/todos")]
public class TasksController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ITaskRepository _taskRepository;
    public TasksController(AuthService authService, ITaskRepository taskRepository)
    {
        _authService = authService;
        _taskRepository = taskRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
       
        User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);

       
        var task = new Tarefa
        {
            Title = request.Title,
            Description = request.Description,
            UserId = user.UserId,
            CreatedAt = DateTime.UtcNow,
            User = user
        };

        _taskRepository.ResetTracker();

        
        await _taskRepository.CreateTaskAsync(task);

        return Ok(task);
    }


    [HttpGet]
    public async Task<IActionResult> GetUserTask(int userId)
    {
        try
        {
            
            User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);
           

            
            var tasks = await _taskRepository.GetTasksByUserIdAsync(user);
          

            return Ok(tasks);
        }
        catch (Exception ex)
        {
            
            
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
 
    public async Task<IActionResult> GetTaskById(int id)
    {
        User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);

        var task = await _taskRepository.GetTaskAsync(id);
        if (task == null || task.UserId != user.UserId)
        {
            return BadRequest("Tarefa não pertence ao usuário"); 
        }

        return Ok(task);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Updatetask(int id, [FromBody] UpdateTaskDto updatedTask)
    {
        User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);

        var existingTask = await _taskRepository.GetTaskAsync(id);

        if(existingTask == null)
        {
            return NotFound("Tarefa não encontrada");
        }

        if(existingTask.UserId != user.UserId)
        {
            return BadRequest("Você não tem permissão para ataulizar esta tarefa");
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Status = updatedTask.Status;


        await _taskRepository.UpdateTaskAsync(existingTask);
        return Ok(existingTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);
        var task = await _taskRepository.GetTaskAsync(id);

        if(task == null)
        {
            return NotFound("Tarefa não encontrada");

        }

        if (task.UserId != user.UserId)
        {
            return BadRequest("Você não tem permissão para deletar esta tarefa");
        }

        await _taskRepository.DeleteTaskAsync(id);

        return NoContent();
    }




}