using AutoMapper;
using BookStore.Domain;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Validators;
using BookStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Services
{
    public class CategoryServiceTests
    {
        readonly Mock<IGenericRepository<Category>> categoryRepoMock;

        public CategoryServiceTests()
        {
            categoryRepoMock = new Mock<IGenericRepository<Category>>();

            categoryRepoMock.Setup(x => x.InsertAsync(It.IsAny<Category>())).ReturnsAsync((Category x) => x);

        }

        [Fact]
        public void InsertCategoryDuplicate()
        {
            var create = new Category()
            {
                CategoryName = "name",
            };

            var service = new CategoryService(
                categoryRepoMock.Object, new CategoryValidator());

            categoryRepoMock.Setup(x => x.IsExistAsync(y => y.CategoryName == create.CategoryName)).ReturnsAsync(true);

            var result = service.InsertCategory(create);
            Assert.Equal("Duplicate, category already exist.", result.Exception.InnerException.Message);

        }

        [Fact]
        public void InsertCategoryNotDuplicate()
        {
            var create = new Category()
            {
                CategoryName = "name",
            };

            var service = new CategoryService(
               categoryRepoMock.Object, new CategoryValidator());

            categoryRepoMock.Setup(x => x.IsExistAsync(y => y.CategoryName == create.CategoryName)).ReturnsAsync(false);

            var result = service.InsertCategory(create);

            Assert.NotNull(result);
            Assert.True(result.Result.CategoryName == create.CategoryName);

        }

    }
}
