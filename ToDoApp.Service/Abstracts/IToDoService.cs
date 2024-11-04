using Core.Responses;
using ToDoApp.Models.Dtos.ToDos.Requests;
using ToDoApp.Models.Dtos.ToDos.Responses;

namespace ToDoApp.Service.Abstracts;

public interface IToDoService
{
    ReturnModel<List<ToDoResponseDto>> GetAll();
    ReturnModel<List<ToDoResponseDto>> GetByUserId(string userId);

    ReturnModel<List<ToDoResponseDto>> GetFilteredTodos(string userId, FilterToDoRequest filter);

    ReturnModel<ToDoResponseDto> GetById(Guid id);
    ReturnModel<ToDoResponseDto> Add(CreateToDoRequest request, string userId);
    ReturnModel<ToDoResponseDto> Update(UpdateToDoRequest request);
    ReturnModel<ToDoResponseDto> Remove(Guid id);
}