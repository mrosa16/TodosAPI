using Microsoft.AspNetCore.Identity.Data;
using TodosAPI.Core.Services;

namespace TodosAPI.Application.UseCases.LoginUser
{
    public class LoginUserHandler
    {
        private readonly AuthService _authService;

        public LoginUserHandler(AuthService authService)
        {  _authService = authService; }

        public async Task<string> Handle(LoginRequest request)
        {
            return await _authService.AuthenticateUserAsync(request.Email, request.Password);
        }
    }
}
