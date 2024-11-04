using AutoMapper;
using Core.Responses;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.Models.Dtos.ToDos.Requests;
using ToDoApp.Models.Dtos.ToDos.Responses;
using ToDoApp.Models.Entities;
using ToDoApp.Service.Abstracts;
using ToDoApp.Service.Rules;

namespace ToDoApp.Service.Concretes;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;
    private readonly ToDoBusinessRules _businessRules;

    public ToDoService(IToDoRepository toDoRepository, IMapper mapper, ToDoBusinessRules rules)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
        _businessRules = rules;
    }
    
    
    public ReturnModel<List<ToDoResponseDto>> GetAll()
    {
        List<ToDo> toDos = _toDoRepository.GetAll();
        List<ToDoResponseDto> responses = _mapper.Map<List<ToDoResponseDto>>(toDos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message= string.Empty,
            StatusCode = 200,
            Success = true
        };
    }
    
    public ReturnModel<List<ToDoResponseDto>> GetByUserId(string userId)
    {
        var toDos = _toDoRepository.GetAll()
            .Where(todo => todo.UserId == userId)
            .ToList();

        var response = _mapper.Map<List<ToDoResponseDto>>(toDos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = response,
            Message = "Kullanıcının yapılacak işleri getirildi.",
            StatusCode = 200,
            Success = true
        };
    }
    
    public ReturnModel<List<ToDoResponseDto>> GetFilteredTodos(string userId, FilterToDoRequest filter)
    {
        var query = _toDoRepository.GetAll().Where(todo => todo.UserId == userId);

        if (filter.StartDate.HasValue)
            query = query.Where(todo => todo.StartDate >= filter.StartDate.Value);

        if (filter.EndDate.HasValue)
            query = query.Where(todo => todo.EndDate <= filter.EndDate.Value);

        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(todo => todo.Title.Contains(filter.Title));
        
        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(todo => todo.Description.Contains(filter.Description));

        if (filter.Priority.HasValue)
            query = query.Where(todo => todo.Priority == filter.Priority.Value);
        
        if (filter.Completed.HasValue)
            query = query.Where(todo => todo.Completed == filter.Completed.Value);

        var toDos = query.ToList();
        var response = _mapper.Map<List<ToDoResponseDto>>(toDos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = response,
            Message = "Filtrelenen todolar getirildi.",
            StatusCode = 200,
            Success = true
        };
    }


    public ReturnModel<ToDoResponseDto> GetById(Guid id)
    {
        var toDo = _toDoRepository.GetById(id);
        _businessRules.ToDoIsNullCheck(toDo);

        var response = _mapper.Map<ToDoResponseDto>(toDo);
        
        return new ReturnModel<ToDoResponseDto>
        {
            Data = response,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Add(CreateToDoRequest request, string userId)
    {
        ToDo createdToDo = _mapper.Map<ToDo>(request);
        createdToDo.Id = Guid.NewGuid();

        _toDoRepository.Add(createdToDo);

        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(createdToDo);

        return new ReturnModel<ToDoResponseDto>
        {
            Data = response,
            Message = "Yeni iş Eklendi.",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Update(UpdateToDoRequest request)
    {
        ToDo toDo = _toDoRepository.GetById(request.Id);

        ToDo update = new ToDo
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Priority = request.Priority,
            Completed = request.Completed,
            CategoryId = toDo.CategoryId,
            UserId = toDo.UserId
        };

        ToDo updatedToDo = _toDoRepository.Update(update);
        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(updatedToDo);

        return new ReturnModel<ToDoResponseDto>()
        {
            Data = response,
            Message = "Yapılacak iş güncellendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Remove(Guid id)
    {
        ToDo toDo = _toDoRepository.GetById(id);
        ToDo deletedToDo = _toDoRepository.Remove(toDo);
        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(deletedToDo);

        return new ReturnModel<ToDoResponseDto>()
        {
            Data = response,
            Message = "Yapılacak iş silindi.",
            StatusCode = 200,
            Success = true
        };
    }
}