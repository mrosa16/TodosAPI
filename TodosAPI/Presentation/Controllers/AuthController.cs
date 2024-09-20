

using Microsoft.AspNetCore.Mvc;
using TodosAPI.Application.UseCases.CreateUser;
using TodosAPI.Core.Services;

namespace TodosAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CreateUserHandler _createUserHandler;
        private readonly AuthService _authService;

        public AuthController(CreateUserHandler createUserHandler, AuthService authService)
        {
            _createUserHandler = createUserHandler;
            _authService = authService;
        }


        //Endpoint Register 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) // Verifica se o modelo é válido
            {
                return BadRequest(ModelState); // Retorna erros de validação
            }
            try
            {
                var user = await _createUserHandler.Handle(request);
                return Ok(new
                {
                    message = "Usuário registado com sucesso",
                    userId = user.UserId
                });
            }
            catch (Exception ex) 
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
