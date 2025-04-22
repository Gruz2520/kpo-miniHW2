using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using kpo_minihw2.Infrastructure.Repositories;
using kpo_minihw2.Application.Interfaces;
using kpo_minihw2.Domain.Events;
using kpo_minihw2.Application.Services;
using Microsoft.AspNetCore.Builder;
using kpo_minihw2.Presentation.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, EnclosureRepository>();
builder.Services.AddSingleton<IFeedingScheduleRepository, FeedingScheduleRepository>();
builder.Services.AddSingleton<IZooStatisticsService, ZooStatisticsService>();
builder.Services.AddSingleton<AnimalTransferService>();
builder.Services.AddSingleton<FeedingOrganizationService>();
builder.Services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddEndpointsApiExplorer();

// Настройка Swagger через метод расширения
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Использование Swagger через метод расширения
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

app.Run();