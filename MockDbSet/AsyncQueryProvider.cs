namespace MockDbSet
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider innerQueryProvider;

        internal AsyncQueryProvider(IQueryProvider innerQueryProvider)
        {
            this.innerQueryProvider = innerQueryProvider;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return this.innerQueryProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return this.innerQueryProvider.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute<TResult>(expression));
        }
    }
}
