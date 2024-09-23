using Microsoft.AspNetCore.Mvc;
using TodosAPI.Core.Services;

namespace TodosAPI.Application.UseCases.CreateTask
{
    public class CreateTaskHandler
    {

        private readonly AuthService _authService;
        private readonly ITaskRepository _taskRepository;
        public CreateTaskHandler(AuthService authService, ITaskRepository taskRepository)
        {
            _authService = authService;
            _taskRepository = taskRepository;
        }

    //    public async Task<IActionResult> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    //    {
    //        // Create the task
    //        var task = new Tarefa { Title = request.Title, Description = request.Description };
    //        await _taskRepository.CreateTaskAsync(task);
    //        await _taskRepository.SaveChangesAsync(cancellationToken);

    //        return new OkObjectResult("Tarefa Criada com sucesso");
    //    }

    }
}
