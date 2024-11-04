using ToDoApp.Models.Dtos.Users.Requests;
using ToDoApp.Models.Entities;

namespace ToDoApp.Service.Abstracts;

public interface IUserService
{
    Task<User> RegisterAsync(RegisterRequestDto dto);
    Task<User> LoginAsync(LoginRequestDto dto);
    Task<User> GetByEmailAsync(string email);
    Task<User> ChangePasswordAsync(string id, ChangePasswordRequestDto dto);
    Task<User> UpdateAsync(string id, UserUpdateRequestDto dto);
    Task<string> DeleteAsync(string id);
}