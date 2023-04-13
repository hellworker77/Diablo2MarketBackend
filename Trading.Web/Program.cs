using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Trading.Web.Middlewares;

namespace Trading.Web
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

            

            builder.Services.AddDbContext<ApplicationContext>(optionsAction =>
            {
                optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("entityDb"),
                    migration => migration.MigrationsAssembly(typeof(Program).Assembly.FullName));
                optionsAction.UseLazyLoadingProxies();
            });

            builder.Services.AddIdentities();
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.Services.AddMappers();
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

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
    }

}