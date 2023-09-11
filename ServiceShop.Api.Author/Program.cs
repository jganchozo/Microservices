using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Author.Persistence;
using ServiceShop.Api.Author.RabbitHandler;
using ServiceShop.Messenger.Email.SendGridLibrary.Implement;
using ServiceShop.Messenger.Email.SendGridLibrary.Interface;
using ServiceShop.RabbitMQ.Bus.Bus;
using ServiceShop.RabbitMQ.Bus.Implement;
using ServiceShop.RabbitMQ.Bus.QueueEvent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    var configuration = sp.GetRequiredService<IConfiguration>();

    return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory, configuration);
});

builder.Services.AddSingleton<IEmail, SendGridEmail>();

builder.Services.AddTransient<EmailEventHandler>();

builder.Services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("postgresqldbconnection");
//builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddDbContext<AuthorContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresqldbconnection"));
});

//builder.Services.AddMediatR(typeof(New.Handler).Assembly);
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IRabbitEventBus>();
eventBus.Subscribe<EmailEventQueue, EmailEventHandler>();

app.Run();
