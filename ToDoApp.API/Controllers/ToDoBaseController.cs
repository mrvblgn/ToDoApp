using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.API.Controllers;

public class ToDoBaseController : ControllerBase
{
    public string GetUserId()
    {
        return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }
}