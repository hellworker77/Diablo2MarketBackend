using Common.ExceptionHandlerFactory;
using Common.Mappers;
using Common.Services;
using Common.Services.Interfaces;
using Dal.Data;
using Dal.Interfaces;
using Dal.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Entities;

namespace Trading.Web;

internal static class ExtensionsMethods
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var identityUrl = configuration.GetValue<string>("IdentityUrl");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = "trading";
        });
        
    }

    public static void AddIdentities(this IServiceCollection service)
    {
        service.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddDefaultTokenProviders();

    }
    public static void AddServices(this IServiceCollection service)
    {
        service.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        service.AddTransient<IIdentityService, IdentityService>();
        service.AddTransient<IDealService, DealService>();
        service.AddTransient<IDealMemberService, DealMemberService>();
        service.AddTransient<IItemService, ItemService>();
        service.AddTransient<IItemAttributeService, ItemAttributeService>();
    }
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddTransient<IDealMemberRepository, DealMemberRepository>();
        service.AddTransient<IDealRepository, DealRepository>();
        service.AddTransient<IItemAttributeRepository, ItemAttributeRepository>();
        service.AddTransient<IItemRepository, ItemRepository>();
    }
    public static void AddMappers(this IServiceCollection service)
    {
        service.AddTransient<DealMemberMapper>();
        service.AddTransient<ItemMapper>();
        service.AddTransient<ItemAttributeMapper>();
        service.AddTransient<DealMapper>();
    }

    public static void AddExceptionHandlers(this IServiceCollection service)
    {
        service.AddTransient<AbstractExceptionResponse, ArgumentExceptionResponse>();
        service.AddTransient<AbstractExceptionResponse, EntityWithIdNotFoundExceptionResponse>();
        service.AddTransient<AbstractExceptionResponse, NullReferenceExceptionResponse>();
        service.AddTransient<AbstractExceptionResponse, PermissionDeniedExceptionResponse>();
        service.AddTransient<AbstractExceptionResponse, BaseExceptionResponse>();
        service.AddTransient(AbstractExceptionResponse.GetResponseHandler);
    }
    public static void AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "Swagger API",
                Title = "Swagger with Identity Server 4",
                Version = "0.0.1"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(configuration.GetValue<string>("IdentityTokenUrl")!),
                        Scopes = new Dictionary<string, string>
                        {
                            {"trading", "trading"}
                        }
                    }
                },
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
    }
}