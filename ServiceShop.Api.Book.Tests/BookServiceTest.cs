using AutoMapper;
using GenFu;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceShop.Api.Book.Application;
using ServiceShop.Api.Book.Model;
using ServiceShop.Api.Book.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceShop.Api.Book.Tests
{
    public class BookServiceTest
    {
        private IEnumerable<MaterialLibrary> GetFakeData()
        {
            A.Configure<MaterialLibrary>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.MaterialLibraryId, () => Guid.NewGuid());

            var list = A.ListOf<MaterialLibrary>(30);
            list[0].MaterialLibraryId = Guid.Empty;

            return list;
        }

        private Mock<LibraryContext> CreateContext()
        {
            var testData = GetFakeData().AsQueryable();
            var dbSet = new Mock<DbSet<MaterialLibrary>>();

            dbSet.As<IQueryable<MaterialLibrary>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<MaterialLibrary>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<MaterialLibrary>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<MaterialLibrary>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            dbSet.As<IAsyncEnumerable<MaterialLibrary>>().Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new AsyncEnumerator<MaterialLibrary>(testData.GetEnumerator()));

            dbSet.As<IQueryable<MaterialLibrary>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<MaterialLibrary>(testData.Provider));

            Mock<LibraryContext> context = new();
            context.Setup(x => x.MaterialLibrary).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async void GetBookById()
        {
            Mock<LibraryContext> mockContext = CreateContext();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });

            var mapper = mapperConfig.CreateMapper();

            FilterSelect.Execute request = new()
            {
                BookId = Guid.Empty
            };

            FilterSelect.Handler handler = new(mockContext.Object, mapper);
            var book = await handler.Handle(request, new CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.MaterialLibraryId == Guid.Empty);
        }

        [Fact]
        public async void GetBooks()
        {

            Mock<LibraryContext> mockContext = CreateContext();

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });

            var mapper = mapperConfig.CreateMapper();

            SelectAll.Handler handler = new(mockContext.Object, mapper);
            SelectAll.Execute request = new();
            var list = await handler.Handle(request, new CancellationToken());

            Assert.True(list.Any());
        }

        [Fact]
        public async void SaveBook()
        {
            //System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "BookDB")
                .Options;

            var context = new LibraryContext(options);

            New.Execute request = new()
            {
                Title = "Microservices with c#",
                BookAuthor = Guid.Empty,
                PublicationDate = DateTime.Now
            };

            New.Handler handler = new(context);
            Unit result = await handler.Handle(request, new CancellationToken());

            Assert.True(result == Unit.Value);
        }
    }
}
