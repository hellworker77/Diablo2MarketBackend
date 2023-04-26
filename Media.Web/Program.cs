using Dal.Data;
using Media.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Media.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
            builder.Services.ConfigureAuthentication(builder.Configuration);

            var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("entityDb"),
                    migration => migration.MigrationsAssembly(migrationAssembly)));

            builder.Services.AddIdentities();
            builder.Services.AddExceptionHandlers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger market");
                    options.DocExpansion(DocExpansion.List);
                    options.OAuthClientId("Api");
                    options.OAuthClientSecret("client_secret");
                });
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}