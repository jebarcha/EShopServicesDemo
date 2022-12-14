using AutoMapper;
using EShopServices.Api.Book.Application;
using EShopServices.Api.Book.Model;
using EShopServices.Api.Book.Persistence;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EShopServices.Api.Book.Tests;

public class BooksServiceTest
{
	private Mock<ContextBookstore> CreateContext()
	{
		var testData = GetTestData().AsQueryable();

		var dbSet = new Mock<DbSet<MaterialBook>>();
		dbSet.As<IQueryable<MaterialBook>>().Setup(x => x.Provider).Returns(testData.Provider);
        dbSet.As<IQueryable<MaterialBook>>().Setup(x => x.Expression).Returns(testData.Expression);
        dbSet.As<IQueryable<MaterialBook>>().Setup(x => x.ElementType).Returns(testData.ElementType);
        dbSet.As<IQueryable<MaterialBook>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

		dbSet.As<IAsyncEnumerable<MaterialBook>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
			.Returns(new AsyncEnumerator<MaterialBook>(testData.GetEnumerator()));
		

		dbSet.As<IQueryable<MaterialBook>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<MaterialBook>(testData.Provider));


		var context = new Mock<ContextBookstore>();
		context.Setup(x => x.MaterialLibrary).Returns(dbSet.Object);
		return context;
	}

	private IEnumerable<MaterialBook> GetTestData()
	{
		// this method is to fill out of data
		A.Configure<MaterialBook>()
			.Fill(x => x.Title).AsArticleTitle()
			.Fill(x => x.MaterialLibraryId, () => { return Guid.NewGuid(); });
		
		var list = A.ListOf<MaterialBook>(30);
		list[0].MaterialLibraryId = Guid.Empty;

		return list;
    }


	[Fact]
	public async void GetBookById_ShouldReturnASingleBook_WhenInvokeHandler()
	{
		// Arrange
		var mockContext = CreateContext();

		var mapConfig = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new MappingTest());
		});

		var mapper = mapConfig.CreateMapper();

		var request = new GetFilter.UniqueBook();
		request.BookId = Guid.Empty;

		var handler = new GetFilter.Handler(mockContext.Object, mapper);

		// Act
		var result = await handler.Handle(request, new System.Threading.CancellationToken());

		// Assert
		Assert.NotNull(result);
		Assert.True(result.MaterialLibraryId == Guid.Empty);
	}

    [Fact]
    public async void GetBooks_ShouldReturnListOfBooks_WhenInvokeHandler()
    {
		//System.Diagnostics.Debugger.Launch();

		// Arrange
		var mockContext = CreateContext();

		var mapConfig = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new MappingTest());
		});

		var mapper = mapConfig.CreateMapper();

		var handler = new GetBook.Handler(mockContext.Object, mapper);

		var request = new GetBook.Execute();

		// Act
		var list = await handler.Handle(request, new System.Threading.CancellationToken());

		// Assert
		Assert.True(list.Any());
	}

	[Fact]
	public async void NewBook_ShouldSaveANewBook_WhenInvokeHandle()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<ContextBookstore>()
			.UseInMemoryDatabase(databaseName: "DataBaseBook")
			.Options;

		var context = new ContextBookstore(options);

		var request = new NewBook.Execute();
		request.Title = "Microservices Book";
		request.AuthorBook = Guid.Empty;
		request.PublishDate = DateTime.UtcNow;

		var handle = new NewBook.Handler(context);

		// Act
		var result = await handle.Handle(request, new System.Threading.CancellationToken());

		// Assert
		//Assert.True(result != null);
		Assert.IsType<MediatR.Unit>(result);
	}
}
