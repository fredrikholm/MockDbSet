namespace MockDbSet
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    internal class AsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        public T Current => this.enumerator.Current;

        object IDbAsyncEnumerator.Current => this.Current;

        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(this.enumerator.MoveNext());
        }
    }
}
