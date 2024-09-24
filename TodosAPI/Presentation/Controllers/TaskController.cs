using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodosAPI.Application.UseCases.CreateTask;
using TodosAPI.Core.Entities;
using TodosAPI.Core.Services;
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
        // Obter as informações do usuário a partir do token JWT
        User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);

        // Criar a tarefa associada ao usuário
        var task = new Tarefa
        {
            Title = request.Title,
            Description = request.Description,
            UserId = user.UserId,
            CreatedAt = DateTime.UtcNow,
            User = user
        };

        _taskRepository.ResetTracker();

        // Salvar a tarefa no banco de dados
        await _taskRepository.CreateTaskAsync(task);

        return Ok(task);
    }


    [HttpGet]
    public async Task<IActionResult> GetUserTask(int userId)
    {
        try
        {
            // Obter o usuário a partir do token JWT
            User user = await _authService.GetUserFromTokenOrFailedAsync(Request.Headers["Authorization"]);
           

            // Chamar o repositório para buscar as tarefas do usuário
            var tasks = await _taskRepository.GetTasksByUserIdAsync(user);
          

            return Ok(tasks);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro ao buscar tarefas: {ex.Message}");
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
            return BadRequest("Tarefa não pertence ao usuário"); // Retorna 404 se a tarefa não for encontrada ou não pertencer ao usuário
        }

        return Ok(task);
    }

   

  

}