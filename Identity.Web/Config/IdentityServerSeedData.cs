using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Identity.Web.Config;

public static class IdentityServerSeedData
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope(); 
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var notExistedClients = IdentityServerConfiguration.GetClients()
            .Where(c => !context.Clients.Select(x => x.ClientId).Contains(c.ClientId)).ToList();

        foreach (var notExistedClient in notExistedClients)
        {
            context.Clients.Add(notExistedClient.ToEntity());

            context.SaveChanges();
        }


        if (!context.IdentityResources.Any())
        {
            foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        var notExistedScopes = IdentityServerConfiguration.GetApiScopes()
            .Where(c => !context.ApiScopes.Select(c => c.Name).Contains(c.Name)).ToList();

        foreach (var notExistedScope in notExistedScopes)
        {
            context.ApiScopes.Add(notExistedScope.ToEntity());

            context.SaveChanges();
        }

        var notExistedResources = IdentityServerConfiguration.GetApiResources()
            .Where(c => !context.ApiResources.Select(c => c.Name).Contains(c.Name)).ToList();

        foreach (var notExistedResource in notExistedResources)
        {
            context.ApiResources.Add(notExistedResource.ToEntity());

            context.SaveChanges();
        }

    }
}