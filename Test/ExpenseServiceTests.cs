using System.Collections.Generic;
using System.Threading.Tasks;
using MockDbSet;
using Moq;
using Test.Data;
using Test.Model;
using Test.Services;
using Xunit;

namespace Test
{
    /// <summary>
    /// The purpose of two test methods is to exercise the extension 
    /// methods ReturnsDbSet and ReturnsDbSetAsync
    /// </summary>
    public class ExpenseServiceTests
    {
        [Fact]
        public void TestGetAll()
        {
            var expenses = new List<Expense> { new Expense { Id = 1 } };
            var mockContext = new Mock<TestContext>();
            mockContext.Setup(p => p.Set<Expense>()).ReturnsDbSet(expenses);
            var service = new ExpenseService(mockContext.Object);

            var result = service.GetAll();

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            var expenses = new List<Expense> { new Expense { Id = 1 } };
            var mockContext = new Mock<TestContext>();
            mockContext.Setup(p => p.Set<Expense>()).ReturnsDbSetAsync(expenses);
            var service = new ExpenseService(mockContext.Object);

            var result = await service.GetAllAsync();

            Assert.Equal(1, result.Count);
        }
    }
}
