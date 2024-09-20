

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TodosAPI.Application.UseCases.CreateUser;
using TodosAPI.Application.UseCases.LoginUser;
using TodosAPI.Core.Services;

namespace TodosAPI.Presentation.Controllers
{

    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly LoginUserHandler _loginUserHandler;
        private readonly CreateUserHandler _createUserHandler;
        private readonly AuthService _authService;
        private object token;

        public AuthController(CreateUserHandler createUserHandler, AuthService authService, LoginUserHandler loginUserHandler)
        {
            _createUserHandler = createUserHandler;
            _authService = authService;
            _loginUserHandler = loginUserHandler;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _createUserHandler.Handle(request);
                var token = _authService.AuthenticateUserAsync(user.Email, request.Password).Result;

                return Ok(new
                {
                    message = "Usuário registado com sucesso",
                    userId = user.UserId,
                    token = token
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            try
            {
                var token = await _loginUserHandler.Handle(request);
                return Ok(new
                {
                    message = "Login realizado com suceeso",
                    token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
