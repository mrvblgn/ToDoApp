using AutoMapper;
using Core.Responses;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.Models.Dtos.Categories.Requests;
using ToDoApp.Models.Dtos.Categories.Responses;
using ToDoApp.Models.Entities;
using ToDoApp.Service.Abstracts;
using ToDoApp.Service.Rules;

namespace ToDoApp.Service.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _categoryBusinessRules;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _categoryBusinessRules = categoryBusinessRules;
    }
    
    
    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        List<Category> toDos = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(toDos);

        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Message= string.Empty,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> GetById(int id)
    {
        var category = _categoryRepository.GetById(id);
        _categoryBusinessRules.CategoryIsNullCheck(category);

        var response = _mapper.Map<CategoryResponseDto>(category);
        
        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> Add(CreateCategoryRequest request)
    {
        Category createdCategory = _mapper.Map<Category>(request);

        _categoryRepository.Add(createdCategory);

        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(createdCategory);

        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = "Yeni kategori eklendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequest request)
    {
        Category category = _categoryRepository.GetById(request.Id);

        Category update = new Category
        {
            Name = request.Name,
            UpdatedDate = category.UpdatedDate
        };

        Category updatedCategory = _categoryRepository.Update(update);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(updatedCategory);

        return new ReturnModel<CategoryResponseDto>()
        {
            Data = response,
            Message = "Kategori güncellendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> Remove(int id)
    {
        Category category = _categoryRepository.GetById(id);
        Category deletedCategory = _categoryRepository.Remove(category);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(deletedCategory);

        return new ReturnModel<CategoryResponseDto>()
        {
            Data = response,
            Message = "Kategori silindi.",
            StatusCode = 200,
            Success = true
        };
    }
}