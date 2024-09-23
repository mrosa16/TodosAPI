using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodosAPI.Application.UseCases.CreateTask;
using TodosAPI.Core.Entities;
using TodosAPI.Core.Services;
using TodosAPI.Infrastructure.Repositories;

[ApiController]
[Route("api/tasks")]
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
            //UserId = user.UserId,
            User = user
        };

        _taskRepository.ResetTracker();

        // Salvar a tarefa no banco de dados
        await _taskRepository.CreateTaskAsync(task);

        return Ok(task);
    }
}