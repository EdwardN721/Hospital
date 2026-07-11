using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Hospital.Domain.Interfaces;

namespace Hospital.Api.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }
    }
}