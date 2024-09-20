using TodosAPI.Core.Entities;
using TodosAPI.Core.Services;

namespace TodosAPI.Application.UseCases.CreateUser
{
    public class CreateUserHandler
    {
        private readonly AuthService _authService;

        public CreateUserHandler(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<User> Handle(CreateUserRequest request)
        {
            return await _authService.RegisterUserAsync(request.Email, request.Password);
        }
    }
}

