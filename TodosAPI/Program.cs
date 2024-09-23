using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodosAPI.Infrastructure.Data;
using TodosAPI.Application.UseCases.CreateUser;
using TodosAPI.Core.Services;
using TodosAPI.Core.Interfaces;
using TodosAPI.Application.UseCases.LoginUser;
using TodosAPI.Infrastructure.Repositories;
using TodosAPI.Application.UseCases.CreateTask;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddDbContext<TaskDbContext>(options =>
                 options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<LoginUserHandler>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<CreateTaskHandler>();
builder.Services.AddScoped<CreateTaskHandler>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
