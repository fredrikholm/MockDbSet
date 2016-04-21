# MockDbSet
__Moq extension methods for mocking DbSet with LINQ queries__

Mocking a DbSet is easy, but if you try to use the mock as-is with your LINQ queries, you will discover that it needs some massaging to work as expected; especially when you use it with async queries. The secret is to setup an implementation of IQueryable for the DbSet and to wire it to our predefined data; making use of the LINQ to Objects provider that works with List&lt;T&gt;.

This lib performs all that plumbing for you, and contains two extension methods that makes mocking your DbSet<T> properties a breeze:
* ReturnsDbSet
* ReturnsAsyncDbSet

## Example usage

    var expenses = new List<Expense> { new Expense { Id = 1 } };
    var mockContext = new Mock<TestContext>();
    mockContext.Setup(p => p.Set<Expense>()).ReturnsDbSet(expenses);


## How do I get a hold of it?

The lib is available as a NuGet package:

    PM> Install-Package MockDbSetExtensions

## Credits
The code in this repo is heavily inspired by (as in _mostly copied from_) other resources:

* [Testing with a mocking framework (EF6 onwards)](https://msdn.microsoft.com/en-us/library/dn314429.aspx) - The main source of inspiration. It also explains the concepts in detail.
* [A Simple interface for fluently mocking a DbSet](http://codethug.com/2015/03/20/mocking-dbset/) - Provided inspiration for the extension methods.
