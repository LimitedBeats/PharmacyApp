using System.Data.Common;

namespace PharmacyConsole.DAL.DataAccess
{
    public class TransactionContext : IDisposable, IAsyncDisposable
    {
        public DbTransaction Transaction { get; private set; }

        public DbConnection Connection { get; private set; }

        public TransactionContext Init(BaseRepository repository)
        {
            Connection = repository.CreateConnection();
            Connection.Open();
            Transaction = Connection.BeginTransaction();
            return this;
        }

        public async Task<TransactionContext> InitAsync(BaseRepository repository)
        {
            return await InitAsync(repository, CancellationToken.None);
        }

        public async Task<TransactionContext> InitAsync(BaseRepository repository, CancellationToken cancellationToken)
        {
            Connection = repository.CreateConnection();
            await Connection.OpenAsync(cancellationToken);
            Transaction = await Connection.BeginTransactionAsync(cancellationToken);
            return this;
        }

        public void Commit()
        {
            Transaction.Commit();
            Transaction = null;
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Transaction = null;
        }

        public void Dispose()
        {
            Rollback();
            Connection.Close();
        }

        public async ValueTask DisposeAsync()
        {
            Rollback();
            await Connection.CloseAsync();
        }
    }
}
