using Microsoft.EntityFrameworkCore;
using Tarefas.API.Aplication;
using Tarefas.API.Data;
using Tarefas.API.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddScoped<ITarefaAplication, TarefaAplication>();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.UseHttpsRedirection();



app.Run();


