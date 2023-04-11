
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Configuration.Repository;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Dal.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ocelot API", Version = "v1" });
            });
            builder.Services.AddOcelot(builder.Configuration);

            builder.Services.AddDbContext<ApplicationContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("entityDb"),
                    migration => migration.MigrationsAssembly(typeof(Program).Assembly.FullName));
                optionsAction.UseLazyLoadingProxies();
            });

            builder.Services.ConfigureAuthentication(builder.Configuration);

            builder.Services.ConfigureDbInitializer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "swagger";
                    c.DocExpansion(DocExpansion.None);
                });

                app.DbInitialize();
            }
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseOcelot().Wait();
            
            app.MapControllers();

            app.Run();
        }
    }
}