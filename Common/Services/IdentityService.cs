using Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Common.Services;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public IdentityService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid GetUserIdentity()
    {
        var userIdentity = _contextAccessor.HttpContext.User.FindFirst("sub")?.Value;

        if (userIdentity == null)
        {
            throw new NullReferenceException("Not found user identity");
        }

        return Guid.Parse(userIdentity);
    }
}