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


    }

	private IEnumerable<MaterialBook> GetTestData()
	{
		A.Configure<MaterialBook>()
			.Fill(x => x.Title).AsArticleTitle()
			.Fill(x => x.MaterialLibraryId, () => { return Guid.NewGuid(); });
		
		var list = A.ListOf<MaterialBook>(30);
		list[0].MaterialLibraryId = Guid.Empty;

		return list;
    }

    [Fact]
    public void GetBooks()
    {
		// Arrange
		var mockContext = new Mock<ContextBookstore>();
		var mockMapper = new Mock<IMapper>();
		var handler = new GetBook.Handler(mockContext.Object, mockMapper.Object);

		// Act
		

		// Assert


	}
}
