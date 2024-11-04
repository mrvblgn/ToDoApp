using AutoMapper;
using ToDoApp.Models.Dtos.Categories.Requests;
using ToDoApp.Models.Dtos.Categories.Responses;
using ToDoApp.Models.Dtos.ToDos.Requests;
using ToDoApp.Models.Dtos.ToDos.Responses;
using ToDoApp.Models.Entities;

namespace ToDoApp.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateToDoRequest, ToDo>();
        CreateMap<UpdateToDoRequest, ToDo>();
        CreateMap<ToDo, ToDoResponseDto>()
            .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
            .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));

        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponseDto>();

    }
}