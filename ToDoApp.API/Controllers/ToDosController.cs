using System.Security.Claims;
using Core.Tokens.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models.Dtos.ToDos.Requests;
using ToDoApp.Service.Abstracts;

namespace ToDoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDosController(IToDoService _toDoService) : ControllerBase
{
    [HttpGet("getall")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var result = _toDoService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("mytodos")]
    [Authorize]
    public IActionResult GetMyToDos()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = _toDoService.GetByUserId(userId);
        return Ok(result);
    }

    [HttpGet("filtermytodos")]
    [Authorize]
    public IActionResult FilterMyTodos([FromQuery] FilterToDoRequest filter)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = _toDoService.GetFilteredTodos(userId, filter);
        return Ok(result);
    }

    
    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute]Guid id)
    {
        var result = _toDoService.GetById(id);
        return Ok(result);
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody] CreateToDoRequest dto)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value;
        var result = _toDoService.Add(dto,userId);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] UpdateToDoRequest dto)
    {
        var result = _toDoService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromRoute]Guid id)
    {
        var result = _toDoService.Remove(id);
        return Ok(result);
    }
}