using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace ServiceShop.Shared.Setup
{
    public class DefaultWebApplication
    {

        public static WebApplication Create(string[] args, Action<WebApplicationBuilder>? webappBuilder = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            webappBuilder?.Invoke(builder);

            return builder.Build();
        }
        public static void Run(WebApplication webApp)
        {
            if (webApp.Environment.IsDevelopment())
            {
                webApp.UseSwagger();
                webApp.UseSwaggerUI();
            }

            webApp.UseHttpsRedirection();

            webApp.UseAuthorization();

            webApp.MapControllers();

            webApp.Run();
        }
    }
}
