using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.ShoppingCart.Persistence;
using ServiceShop.Api.ShoppingCart.RemoteInterface;
using ServiceShop.Api.ShoppingCart.RemoteService;
//using ServiceShop.Shared.Setup;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("mysqlserverdbconnection")!;
builder.Services.AddDbContext<CartContext>(x => x.UseMySQL(connectionString));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient("Books", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Books")!);
});

//var app = DefaultWebApplication.Create(args, webappBuilder =>
//{
//    // Add services to the container.
//    webappBuilder.Services.AddScoped<IBookService, BookService>();

//    var connectionString = webappBuilder.Configuration.GetConnectionString("mysqlserverdbconnection")!;

//    webappBuilder.Services.AddDbContext<CartContext>(x => x.UseMySQL(connectionString));
//    webappBuilder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//    webappBuilder.Services.AddHttpClient("Books", config =>
//    {
//        config.BaseAddress = new Uri(webappBuilder.Configuration.GetValue<string>("Services:Books")!);
//    });
//});

// Configure the HTTP request pipeline.
//DefaultWebApplication.Run(app);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
