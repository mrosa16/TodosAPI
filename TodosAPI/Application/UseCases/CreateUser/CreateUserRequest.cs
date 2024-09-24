namespace TodosAPI.Application.UseCases.CreateUser
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
