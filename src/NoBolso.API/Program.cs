using Microsoft.EntityFrameworkCore;
using NoBolso.Application;
using NoBolso.API.Middlewares;
using NoBolso.Infrastructure;
using NoBolso.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Registra os serviços da Application (MediatR, Validators, etc.)
builder.Services.AddApplication();

// 2. Registra os serviços da Infrastructure (DbContext, Repositories, EventService, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// O middleware de exceção deve ser um dos primeiros
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Aplicar migrations na inicialização
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<NoBolsoDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations do banco de dados.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
