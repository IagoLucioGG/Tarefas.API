using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tarefas.API.Aplication;
using Tarefas.API.Data;
using Tarefas.API.Interface;
using Tarefas.API.Middleware;
using Tarefas.API.Secutity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddScoped<ITarefaAplication, TarefaAplication>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUsuarioAplication, UsuarioAplication>();
builder.Services.AddScoped<IAuthAplication, AuthAplication>();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var Chave = builder.Configuration.GetValue<string>("ChaveSeguranca");
var key = Encoding.ASCII.GetBytes(Chave ?? "CHAVE_SUPER_SECRETA_AQUI");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.UseHttpsRedirection();



app.Run();


