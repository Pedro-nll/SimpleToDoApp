using API.Repositories;
using API.Repositories.Interfaces;
using API.Usecases;
using API.Usecases.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddDbContext<ReminderDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
    builder.Services.AddScoped<IRemindersGetByIdUseCase, RemindersGetByIdUseCase>();
    builder.Services.AddScoped<IRemindersCreateUseCase, RemindersCreateUseCase>();
    builder.Services.AddScoped<IRemindersDeleteUseCase, RemindersDeleteUseCase>();
    builder.Services.AddScoped<IRemindersGetAllUseCase, RemindersGetAllUseCase>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseCors(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    app.UseExceptionHandler("/error");
    app.UseRouting();
    app.UseAuthorization();
    
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();