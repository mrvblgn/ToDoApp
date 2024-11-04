using ToDoApp.Models.Dtos.Tokens.Responses;
using ToDoApp.Models.Entities;

namespace ToDoApp.Service.Abstracts;

public interface IJwtService
{
    Task<TokenResponseDto> CreateJwtTokenAsync(User user);
}