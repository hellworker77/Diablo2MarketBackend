
using Dal.Data;
using Identity.Web.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities;

namespace Identity.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("_AllowAll",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("entityDb"),
                    migration => migration.MigrationsAssembly(migrationAssembly)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();


            builder.Services.AddIdentityServer(options =>
                { 
                    options.UserInteraction.LoginUrl = null; 
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = context =>
                        context.UseNpgsql(builder.Configuration.GetConnectionString("configurationDb"),
                            migration => migration.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = context =>
                        context.UseNpgsql(builder.Configuration.GetConnectionString("operationalDb"),
                            migration => migration.MigrationsAssembly(migrationAssembly));
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>();
            

            var app = builder.Build();

            app.InitializeDatabase();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("_AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.MapControllers();

            app.Run();
        }
    }
}