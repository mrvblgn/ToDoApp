using AutoMapper;
using Core.Exceptions;
using Moq;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.Models.Dtos.ToDos.Requests;
using ToDoApp.Models.Dtos.ToDos.Responses;
using ToDoApp.Models.Entities;
using ToDoApp.Service.Concretes;
using ToDoApp.Service.Rules;

namespace Service.Tests;

public class ToDoServiceTest
{
    private ToDoService _toDoService;
    private Mock<IMapper> _mapperMock;
    private Mock<IToDoRepository> _repositoryMock;
    private Mock<ToDoBusinessRules> _rulesMock;


    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IToDoRepository>();
        _mapperMock = new Mock<IMapper>();
        _rulesMock = new Mock<ToDoBusinessRules>();
        _toDoService = new ToDoService(_repositoryMock.Object, _mapperMock.Object, _rulesMock.Object);
    }

    [Test]
    public void GetAll_ReturnsSuccess()
    {
        // Arange
        List<ToDo> posts = new List<ToDo>();
        List<ToDoResponseDto> responses = new();
        _repositoryMock.Setup(x => x.GetAll(null, true)).Returns(posts);
        _mapperMock.Setup(x => x.Map<List<ToDoResponseDto>>(posts)).Returns(responses);

        // Act 

        var result = _toDoService.GetAll();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(responses, result.Data);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(string.Empty, result.Message);
    }

    [Test]
    public void Add_WhenToDoAdded_ReturnsSuccess()
    {
        // Arange
        CreateToDoRequest dto =
            new CreateToDoRequest("Deneme", "Content", DateTime.Now, DateTime.Today, (Priority)1, 1);
        ToDo toDo = new ToDo
        {
            Id = new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
            UserId = "{5C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}",
            Title = "Deneme",
            Description = "Deneme",
            CategoryId = 100,
            CreatedDate = DateTime.Now
        };

        ToDoResponseDto response = new ToDoResponseDto
        {
            Id = new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
            Category = "Deneme",
            Description = "Deneme",
            CreatedDate = DateTime.Now,
            Title = "Deneme",
            UserName = "Talhişko"
        };

        _mapperMock.Setup(x => x.Map<ToDo>(dto)).Returns(toDo);
        _repositoryMock.Setup(x => x.Add(toDo)).Returns(toDo);
        _mapperMock.Setup(x => x.Map<ToDoResponseDto>(toDo)).Returns(response);


        // Act
        var result = _toDoService.Add(dto, "{5C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}");

        //Assert
        Assert.AreEqual(response, result.Data);
        Assert.IsTrue(result.Success);


    }

    [Test]
    public void GetById_WhenToDoIsNotPresent_ThrowsException()
    {
        // Arange 
        Guid id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}");
        ToDo toDo = null;
        _rulesMock.Setup(x => x.ToDoIsNullCheck(toDo)).Throws(new NotFoundException("İlgili todo bulunamadı."));




        // Assert
        Assert.Throws<NotFoundException>(() => _toDoService.GetById(id), "İlgili todo bulunamadı.");
    }


    [Test]
    public void GetById_WhenToDoIsPresent_ReturnsSuccess()
    {
        ToDo toDo = new ToDo
        {
            Id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}")
        };

        Guid id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}");

        ToDoResponseDto response = new ToDoResponseDto()
        {
            Id = new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
            Title = "Deneme",
            Description = "Deneme",
            CreatedDate = DateTime.Now
        };

        _repositoryMock.Setup(x => x.GetById(id)).Returns(toDo);
        _rulesMock.Setup(x => x.ToDoIsNullCheck(toDo));
        _mapperMock.Setup(x => x.Map<ToDoResponseDto>(toDo)).Returns(response);


        var result = _toDoService.GetById(id);


        Assert.AreEqual(response, result.Data);
        Assert.IsTrue(result.Success);
    }
}