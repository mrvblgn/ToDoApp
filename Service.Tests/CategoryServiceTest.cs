using AutoMapper;
using Moq;
using Core.Exceptions;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.Models.Dtos.Categories.Requests;
using ToDoApp.Models.Dtos.Categories.Responses;
using ToDoApp.Models.Entities;
using ToDoApp.Service.Concretes;
using ToDoApp.Service.Rules;

namespace Service.Tests
{
    public class CategoryServiceTest
    {
        private CategoryService _categoryService;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<CategoryBusinessRules> _categoryBusinessRulesMock;

        [SetUp]
        public void SetUp()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _categoryBusinessRulesMock = new Mock<CategoryBusinessRules>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object, _categoryBusinessRulesMock.Object);
        }

        [Test]
        public void GetAll_ReturnsSuccess()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" }
            };
            var categoryResponseDtos = new List<CategoryResponseDto>
            {
                new CategoryResponseDto { Id = 1, Name = "Category1" },
                new CategoryResponseDto { Id = 2, Name = "Category2" }
            };

            _categoryRepositoryMock.Setup(repo => repo.GetAll(null, true)).Returns(categories);
            _mapperMock.Setup(m => m.Map<List<CategoryResponseDto>>(categories)).Returns(categoryResponseDtos);

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(categoryResponseDtos, result.Data);
        }

        [Test]
        public void GetById_WhenCategoryExists_ReturnsSuccess()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category1" };
            var categoryResponseDto = new CategoryResponseDto { Id = 1, Name = "Category1" };

            _categoryRepositoryMock.Setup(repo => repo.GetById(1)).Returns(category);
            _categoryBusinessRulesMock.Setup(rules => rules.CategoryIsNullCheck(category));
            _mapperMock.Setup(m => m.Map<CategoryResponseDto>(category)).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.GetById(1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(categoryResponseDto, result.Data);
        }

        [Test]
        public void GetById_WhenCategoryDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            Category category = null;
            _categoryRepositoryMock.Setup(repo => repo.GetById(1)).Returns(category);
            _categoryBusinessRulesMock.Setup(rules => rules.CategoryIsNullCheck(category)).Throws(new NotFoundException("Category not found."));

            // Assert
            Assert.Throws<NotFoundException>(() => _categoryService.GetById(1));
        }

        [Test]
        public void Add_WhenCategoryIsCreated_ReturnsSuccess()
        {
            // Arrange
            var createRequest = new CreateCategoryRequest("New Category");
            var createdCategory = new Category { Id = 1, Name = "New Category" };
            var categoryResponseDto = new CategoryResponseDto { Id = 1, Name = "New Category" };

            _mapperMock.Setup(m => m.Map<Category>(createRequest)).Returns(createdCategory);
            _categoryRepositoryMock.Setup(repo => repo.Add(createdCategory)).Returns(createdCategory);
            _mapperMock.Setup(m => m.Map<CategoryResponseDto>(createdCategory)).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.Add(createRequest);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Yeni kategori eklendi", result.Message);
            Assert.AreEqual(categoryResponseDto, result.Data);
        }

        [Test]
        public void Update_WhenCategoryIsUpdated_ReturnsSuccess()
        {
            // Arrange
            var updateRequest = new UpdateCategoryRequest(1, "Updated Category");
            var existingCategory = new Category { Id = 1, Name = "Old Category" };
            var updatedCategory = new Category { Id = 1, Name = "Updated Category" };
            var categoryResponseDto = new CategoryResponseDto { Id = 1, Name = "Updated Category" };

            _categoryRepositoryMock.Setup(repo => repo.GetById(updateRequest.Id)).Returns(existingCategory);
            _mapperMock.Setup(m => m.Map<Category>(updateRequest)).Returns(updatedCategory);
            _categoryRepositoryMock.Setup(repo => repo.Update(updatedCategory)).Returns(updatedCategory);
            _mapperMock.Setup(m => m.Map<CategoryResponseDto>(updatedCategory)).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.Update(updateRequest);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Kategori güncellendi", result.Message);
            Assert.AreEqual(categoryResponseDto, result.Data);
        }

        [Test]
        public void Remove_WhenCategoryIsDeleted_ReturnsSuccess()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Silinecek kategori" };
            var categoryResponseDto = new CategoryResponseDto { Id = 1, Name = "Silinecek kategori" };

            _categoryRepositoryMock.Setup(repo => repo.GetById(1)).Returns(category);
            _categoryRepositoryMock.Setup(repo => repo.Remove(category)).Returns(category);
            _mapperMock.Setup(m => m.Map<CategoryResponseDto>(category)).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.Remove(1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Kategori silindi.", result.Message);
            Assert.AreEqual(categoryResponseDto, result.Data);
        }
    }
}
