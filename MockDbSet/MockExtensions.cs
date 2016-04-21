using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Moq;
using Moq.Language;
using Moq.Language.Flow;

namespace MockDbSet
{
    public static class MockExtensions
    {
        private static Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
            return mockSet;
        }

        private static Mock<DbSet<T>> CreateMockDbSetForAsync<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new AsyncEnumerator<T>(queryableData.GetEnumerator()));
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<T>(queryableData.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
            return mockSet;
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
            this IReturns<TContext, DbSet<TEntity>> setup,
            IEnumerable<TEntity> entities)
        where TEntity : class
        where TContext : DbContext
        {
            var mockSet = CreateMockDbSet(entities);
            return setup.Returns(mockSet.Object);
        }

        public static IReturnsResult<TContext> ReturnsAsyncDbSet<TEntity, TContext>(
            this IReturns<TContext, DbSet<TEntity>> setup,
            IEnumerable<TEntity> entities)
        where TEntity : class
        where TContext : DbContext
        {
            var mockSet = CreateMockDbSetForAsync(entities);
            return setup.Returns(mockSet.Object);
        }
    }
}
