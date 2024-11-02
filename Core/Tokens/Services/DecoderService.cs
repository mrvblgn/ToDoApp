using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Tokens.Services;

public class DecoderService(IHttpContextAccessor httpContextAccessor)
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }
}